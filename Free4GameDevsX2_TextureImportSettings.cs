using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using Free4GameDevsX2;


public class TexturePostProcessor : AssetPostprocessor
{

	void OnPreprocessTexture()
	{
		TextureImporter textureImporter = (TextureImporter)assetImporter;
		if (assetPath.Contains("Free4GameDevsX2Scaler/InputFolder"))
        {
			textureImporter.textureType = TextureImporterType.Sprite;
			textureImporter.alphaIsTransparency = false;
			textureImporter.filterMode = FilterMode.Point;
			textureImporter.isReadable = true;
			textureImporter.spriteImportMode = SpriteImportMode.None;
			textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
			textureImporter.npotScale = TextureImporterNPOTScale.None;
        }

	}
	
}
