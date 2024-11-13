from ultralytics import YOLO

# Charger le modèle YOLO pré-entrainé (par exemple YOLOv8s)
model = YOLO("yolo11n.pt")  # Remplace "yolov8s.pt" par le modèle de ton choix

# Chemin vers le fichier de configuration de ton dataset
dataset_path = "dataset/data.yaml"  # Assure-toi que ce chemin est correct

# Entraînement du modèle
model.train(
    data=dataset_path,
    epochs=5,        # Nombre d'époques
    batch=128,         # Taille du batch (ajuste selon la capacité de ta machine)
    name="custom_yolo_model"  # Nom du dossier où seront enregistrés les résultats
)
