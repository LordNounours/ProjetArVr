import os
import numpy as np
from tensorflow.keras.preprocessing.image import ImageDataGenerator, img_to_array, load_img, array_to_img, save_img
import cv2  # Assurez-vous d'installer OpenCV pour le traitement d'image

# Chemins d'entrée et de sortie
input_dir = 'test/'  # Remplacez par le chemin vers votre dossier d'images
output_dir = 'augmented/'  # Remplacez par le chemin vers votre dossier de sortie
print(f"Input directory: {input_dir}")
print(f"Output directory: {output_dir}")
if not os.path.exists(output_dir):
    os.makedirs(output_dir)

# Générateur d'images avec des transformations
datagen = ImageDataGenerator(
    rotation_range=40,
    width_shift_range=0.2,
    height_shift_range=0.2,
    shear_range=0.2,
    zoom_range=(0.8, 1.2),  # Une plage de zoom entre 80% et 120%
    horizontal_flip=True,
    fill_mode='nearest'
)



# Fonction pour ajuster les boîtes englobantes
def adjust_bounding_boxes(original_img, transformed_img, boxes):
    original_width, original_height = original_img.size
    transformed_width, transformed_height = transformed_img.size

    new_boxes = []

    for box in boxes:
        class_id = int(box[0])
        x_center = float(box[1])
        y_center = float(box[2])
        width = float(box[3])
        height = float(box[4])

        # Calculer les coordonnées de la boîte englobante originale
        x_min = (x_center - width / 2) * original_width
        y_min = (y_center - height / 2) * original_height
        x_max = (x_center + width / 2) * original_width
        y_max = (y_center + height / 2) * original_height

        # Appliquer les transformations
        dx = np.random.uniform(-datagen.width_shift_range * original_width, datagen.width_shift_range * original_width)
        dy = np.random.uniform(-datagen.height_shift_range * original_height, datagen.height_shift_range * original_height)

        x_min += dx
        x_max += dx
        y_min += dy
        y_max += dy

        # Rotation
        angle = np.random.choice([0, 90])  # Choisir un angle de rotation
        if angle == 90:
            x_min, y_min = original_height - y_max, x_min
            x_max, y_max = original_height - y_min, x_max

        # Zoom
        zoom_factor = np.random.uniform(datagen.zoom_range[0], datagen.zoom_range[1])

        width_zoomed = (x_max - x_min) * zoom_factor
        height_zoomed = (y_max - y_min) * zoom_factor

        x_center_new = (x_min + x_max) / 2
        y_center_new = (y_min + y_max) / 2

        x_min = x_center_new - width_zoomed / 2
        x_max = x_center_new + width_zoomed / 2
        y_min = y_center_new - height_zoomed / 2
        y_max = y_center_new + height_zoomed / 2

        # Re-normaliser
        x_center_new = (x_min + x_max) / (2 * transformed_width)
        y_center_new = (y_min + y_max) / (2 * transformed_height)
        width_new = (x_max - x_min) / transformed_width
        height_new = (y_max - y_min) / transformed_height

        # S'assurer que les valeurs sont comprises entre 0 et 1
        x_center_new = max(0, min(1, x_center_new))
        y_center_new = max(0, min(1, y_center_new))
        width_new = max(0, min(1, width_new))
        height_new = max(0, min(1, height_new))

        new_boxes.append([class_id, x_center_new, y_center_new, width_new, height_new])

    return new_boxes


# Nombre d'images augmentées à générer par image d'origine
num_augmented_images = 20

# Parcourir toutes les classes (sous-dossiers)
for class_name in os.listdir(input_dir):
    class_dir = os.path.join(input_dir, class_name)
    if os.path.isdir(class_dir):
        # Créer un dossier pour la classe dans le dossier de sortie
        output_class_dir = os.path.join(output_dir, class_name)
        if not os.path.exists(output_class_dir):
            os.makedirs(output_class_dir)

        # Parcourir toutes les images de la classe
        for filename in os.listdir(class_dir):
            if filename.endswith('.jpg') or filename.endswith('.png'):
                img_path = os.path.join(class_dir, filename)
                txt_path = os.path.splitext(img_path)[0] + '.txt'

                if not os.path.exists(txt_path):
                    continue

                # Charger l'image et l'annotation
                img = load_img(img_path)
                x = img_to_array(img)

              
                

                x = x.reshape((1,) + x.shape)

                # Charger les annotations
                with open(txt_path, 'r') as file:
                    boxes = [line.strip().split() for line in file]

               


                # Créer un générateur
                i = 0
                for batch in datagen.flow(x, batch_size=1):
                    augmented_img = array_to_img(batch[0])
                    augmented_img_path = os.path.join(output_class_dir, f'aug_{i}_{filename}')
                    save_img(augmented_img_path, augmented_img)

                    # Ajuster les boîtes englobantes pour l'image augmentée
                    new_boxes = adjust_bounding_boxes(img, augmented_img, boxes)

                    # Sauvegarder les annotations mises à jour
                    augmented_txt_path = os.path.splitext(augmented_img_path)[0] + '.txt'
                    with open(augmented_txt_path, 'w') as file:
                        for box in new_boxes:
                            file.write(' '.join(map(str, box)) + '\n')

                    i += 1
                    if i >= num_augmented_images:
                        break  # On arrête après avoir généré le nombre d'images souhaité

print("Augmentation des images et mise à jour des annotations terminée.")
