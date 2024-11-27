import torch
import torch.onnx
from yolov11.models import yolov11  # Importe votre modèle YOLOv11 ici

# Charger un modèle YOLOv11 préalablement entraîné (sans relancer l'entraînement)
model = yolov11()  # Instancie le modèle YOLOv11, mais sans entraînement
model.load_state_dict(torch.load('best.pt'))  # Charge les poids du modèle déjà entraîné

# Mettre le modèle en mode évaluation (désactive les couches comme Dropout, BatchNorm)
model.eval()

# Créez un exemple d'entrée de forme (1, 3, 640, 640)
dummy_input = torch.randn(1, 3, 320, 320)  # 1 image, 3 canaux, 640x640 pixels

# Exportation du modèle vers ONNX
torch.onnx.export(
    model,                       # Le modèle à exporter
    dummy_input,                 # L'entrée fictive pour déterminer les dimensions
    "best0.onnx",                 # Le chemin où le modèle ONNX sera enregistré
    verbose=True,                # Afficher des détails pendant l'exportation
    input_names=["input"],       # Nom de l'entrée du modèle
    output_names=["output"],     # Nom de la sortie du modèle
    opset_version=12,            # Utilisation de la version d'ONNX opset 12 pour la compatibilité
    do_constant_folding=True,    # Activer le repliement constant pour améliorer les performances
    dynamic_axes={"input": {0: "batch_size", 2: "height", 3: "width"}},  # Permet des tailles d'entrée dynamiques
    keep_initializers_as_input=False  # Ne pas garder les initialiseurs comme entrées
)