#!/bin/bash

# Dossier contenant les fichiers .txt
directory="train/labels/sakuretsu"

# Vérification de l'existence du répertoire
if [ ! -d "$directory" ]; then
    echo "Le répertoire spécifié n'existe pas."
    exit 1
fi

# Parcours des fichiers .txt dans le dossier spécifié
for file in "$directory"/*.txt; do
    # Vérifie si le fichier existe et est non vide
    if [ -f "$file" ]; then
        #echo "Modification du fichier: $file"
        # Remplace le premier chiffre par '0'
        sed -i 's/^[0-9]\+/14/' "$file"
        #echo "Fichier modifié: $file"
    else
        echo "Aucun fichier trouvé dans $directory"
    fi
done
