using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UndertaleModLib.Util;

EnsureDataLoaded();

string texFolder = GetFolder(FilePath) + "tilesets" + Path.DirectorySeparatorChar;
TextureWorker worker = new TextureWorker();

Directory.CreateDirectory(texFolder);

SetProgressBar(null, "Tilesets", 0, Data.Backgrounds.Count);
StartProgressBarUpdater();

await DumpTilesets();

// worker.Cleanup();

await StopProgressBarUpdater();
HideProgressBar();
ScriptMessage("Export Complete.\n\nLocation: " + texFolder);

string GetFolder(string path)
{
    return Path.GetDirectoryName(path) + Path.DirectorySeparatorChar;
}

async Task DumpTilesets()
{
    await Task.Run(() => Parallel.ForEach(Data.Backgrounds, DumpTileset));
}

void DumpTileset(UndertaleBackground tileset)
{
    Directory.CreateDirectory(texFolder + tileset.Name.Content);

    if (tileset.Texture != null)
        worker.ExportAsPNG(
            tileset.Texture,
            texFolder + tileset.Name.Content + Path.DirectorySeparatorChar + "output_tileset.png"
        );

    using (
        StreamWriter writer = new StreamWriter(
            texFolder
                + tileset.Name.Content
                + Path.DirectorySeparatorChar
                + tileset.Name.Content
                + ".yy"
        )
    )
    {
        writer.WriteLine("{");
        writer.WriteLine("  \"resourceType\": \"GMTileSet\",");
        writer.WriteLine("  \"resourceVersion\": \"1.0\",");
        writer.WriteLine("  \"name\": \"" + tileset.Name.Content + "\",");
        writer.WriteLine("  \"autoTileSets\": [],");
        writer.WriteLine("  \"macroPageTiles\": {");
        writer.WriteLine("    \"SerialiseHeight\": 0,");
        writer.WriteLine("    \"SerialiseWidth\": 0,");
        writer.WriteLine("    \"TileSerialiseData\": [],");
        writer.WriteLine("  },");
        writer.WriteLine("  \"out_columns\": " + tileset.GMS2TileColumns + ",");
        writer.WriteLine("  \"out_tilehborder\": " + tileset.GMS2OutputBorderX + ",");
        writer.WriteLine("  \"out_tilevborder\": "+tileset.GMS2OutputBorderY+",");
        writer.WriteLine("  \"parent\": {");
        writer.WriteLine("    \"name\": \"Tile Sets\",");
        writer.WriteLine("    \"path\": \"folders/Tile Sets.yy\",");
        writer.WriteLine("  },");
        // TODO Create Sprite Tileset

        string spriteFolder = GetFolder(FilePath) + "sprites" + Path.DirectorySeparatorChar;
        Directory.CreateDirectory(spriteFolder);
        Directory.CreateDirectory(spriteFolder + "spr_" + tileset.Name.Content);

        using (
            StreamWriter writer_sprite = new StreamWriter(
                spriteFolder + "spr_"
                + tileset.Name.Content
                + Path.DirectorySeparatorChar
                + "spr_"
                + tileset.Name.Content
                + ".yy"
            )
        )
        {
            // BEGIN : Extraction Images
            string layer_directory = spriteFolder + "spr_" + tileset.Name.Content + Path.DirectorySeparatorChar + "layers";
            Directory.CreateDirectory(layer_directory);

            // Extraction de l'image à la base du répertoire
            if (tileset.Texture != null)
            {

                // Extraction partie spr
                worker.ExportAsPNG(
                    tileset.Texture,
                    spriteFolder + "spr_" + tileset.Name.Content + Path.DirectorySeparatorChar + "spr_" + tileset.Name.Content + "_0.png"
                );

                // Extraction de l'image "layer"
                Directory.CreateDirectory(layer_directory + Path.DirectorySeparatorChar + "spr_" + tileset.Name.Content + "_0");
                worker.ExportAsPNG(
                    tileset.Texture,
                    layer_directory + Path.DirectorySeparatorChar + "spr_" + tileset.Name.Content + "_0" + Path.DirectorySeparatorChar + "spr_" + tileset.Name.Content + "_layer.png"
                );
            }
            // END : Extraction Images

            writer_sprite.WriteLine("{");
            writer_sprite.WriteLine("  \"resourceType\": \"GMSprite\",");
            writer_sprite.WriteLine("  \"resourceVersion\": \"1.0\",");
            writer_sprite.WriteLine("  \"name\": \"" + "spr_" + tileset.Name.Content + "\",");
            writer_sprite.WriteLine("  \"bbox_bottom\": " + (tileset.Texture.SourceHeight - 1) + ",");
            writer_sprite.WriteLine("  \"bbox_left\": " + 0 + ",");
            writer_sprite.WriteLine("  \"bbox_right\": " + (tileset.Texture.SourceWidth - 1) + ",");
            writer_sprite.WriteLine("  \"bbox_top\": " + 0 + ",");
            writer_sprite.WriteLine("  \"bboxMode\": " + 0 + ",");
            writer_sprite.WriteLine("  \"collisionKind\": 1,");
            writer_sprite.WriteLine("  \"collisionTolerance\": 0,");
            writer_sprite.WriteLine("  \"DynamicTexturePage\": false,");
            writer_sprite.WriteLine("  \"edgeFiltering\": false,");
            writer_sprite.WriteLine("  \"For3D\": false,");
            // BEGIN : frames
            writer_sprite.WriteLine("  \"frames\": [");
            writer_sprite.WriteLine(
                "    {\"resourceType\":\"GMSpriteFrame\",\"resourceVersion\":\"1.1\",\"name\":\""
                + "spr_" + tileset.Name.Content + "_" + 0 + "\",},"
            );
            writer_sprite.WriteLine("  ],");
            // END : frames
            writer_sprite.WriteLine("  \"gridX\": 0,");
            writer_sprite.WriteLine("  \"gridY\": 0,");
            writer_sprite.WriteLine("  \"height\": " + tileset.Texture.SourceHeight + ",");
            writer_sprite.WriteLine("  \"HTile\": false,");
            // BEGIN : layers
            writer_sprite.WriteLine("  \"layers\": [");
            writer_sprite.WriteLine(
                "    {\"resourceType\":\"GMImageLayer\",\"resourceVersion\":\"1.0\",\"name\":\""
                + "spr_" + tileset.Name.Content + "_" + "layer"
                + "\",\"blendMode\":0,\"displayName\":\"default\",\"isLocked\":false,\"opacity\":100.0,\"visible\":true,},"
            );
            writer_sprite.WriteLine("  ],");
            // END : layers
            writer_sprite.WriteLine("  \"nineSlice\": null,");
            writer_sprite.WriteLine("  \"origin\": 0,");
            writer_sprite.WriteLine("  \"parent\": {");
            writer_sprite.WriteLine("    \"name\": \"Sprites\",");
            writer_sprite.WriteLine("    \"path\": \"folders/Sprites.yy\",");
            writer_sprite.WriteLine("  },");
            writer_sprite.WriteLine("  \"preMultiplyAlpha\": false,");
            writer_sprite.WriteLine("  \"sequence\": {");
            writer_sprite.WriteLine("    \"resourceType\": \"GMSequence\",");
            writer_sprite.WriteLine("    \"resourceVersion\": \"1.4\",");
            writer_sprite.WriteLine("    \"name\": \"" + "spr_" + tileset.Name.Content + "\",");
            writer_sprite.WriteLine("    \"autoRecord\": true,");
            writer_sprite.WriteLine("    \"backdropHeight\": 1080,");
            writer_sprite.WriteLine("    \"backdropImageOpacity\": 0.5,");
            writer_sprite.WriteLine("    \"backdropImagePath\": \"\",");
            writer_sprite.WriteLine("    \"backdropWidth\": 1920,");
            writer_sprite.WriteLine("    \"backdropXOffset\": 0.0,");
            writer_sprite.WriteLine("    \"backdropYOffset\": 0.0,");
            writer_sprite.WriteLine(
                "    \"events\": {\"resourceType\":\"KeyframeStore<MessageEventKeyframe>\",\"resourceVersion\":\"1.0\",\"Keyframes\":[],},"
            );
            writer_sprite.WriteLine("    \"eventStubScript\": null,");
            writer_sprite.WriteLine("    \"eventToFunction\": {},");
            writer_sprite.WriteLine("    \"length\": " + "1.0" + ",");
            writer_sprite.WriteLine("    \"lockOrigin\": false,");
            writer_sprite.WriteLine(
                "    \"moments\": {\"resourceType\":\"KeyframeStore<MomentsEventKeyframe>\",\"resourceVersion\":\"1.0\",\"Keyframes\":[],},"
            );
            writer_sprite.WriteLine("    \"playback\": 1,");
            writer_sprite.WriteLine(
                "    \"playbackSpeed\": " + "30.0" + ","
            );
            writer_sprite.WriteLine(
                "    \"playbackSpeedType\": " + "0" + ","
            );
            writer_sprite.WriteLine("    \"showBackdrop\": true,");
            writer_sprite.WriteLine("    \"showBackdropImage\": false,");
            writer_sprite.WriteLine("    \"timeUnits\": 1,");
            // BEGIN : tracks
            writer_sprite.WriteLine("    \"tracks\": [");
            writer_sprite.WriteLine(
                "      {\"resourceType\":\"GMSpriteFramesTrack\",\"resourceVersion\":\"1.0\",\"name\":\"frames\",\"builtinName\":0,\"events\":[],\"inheritsTrackColour\":true,\"interpolation\":1,\"isCreationTrack\":false,\"keyframes\":{\"resourceType\":\"KeyframeStore<SpriteFrameKeyframe>\",\"resourceVersion\":\"1.0\",\"Keyframes\":["
            );
            writer_sprite.WriteLine(
                "            {\"resourceType\":\"Keyframe<SpriteFrameKeyframe>\",\"resourceVersion\":\"1.0\",\"Channels\":{\"0\":{\"resourceType\":\"SpriteFrameKeyframe\",\"resourceVersion\":\"1.0\",\"Id\":{\"name\":\""
                + "spr_" + tileset.Name.Content
                + "\",\"path\":\"sprites/"
                + "spr_" + tileset.Name.Content
                + "/"
                + "spr_" + tileset.Name.Content
                + ".yy\",},},},\"Disabled\":false,\"IsCreationKey\":false,\"Key\":0.0,\"Length\":1.0,\"Stretch\":false,},"
            );
            writer_sprite.WriteLine(
                "          ],},\"modifiers\":[],\"spriteId\":null,\"trackColour\":0,\"tracks\":[],\"traits\":0,},"
            );
            writer_sprite.WriteLine("    ],");
            // END : tracks
            writer_sprite.WriteLine("    \"visibleRange\": null,");
            writer_sprite.WriteLine("    \"volume\": 1.0,");
            writer_sprite.WriteLine("    \"xorigin\": " + "0" + ",");
            writer_sprite.WriteLine("    \"yorigin\": " + "0" + ",");
            writer_sprite.WriteLine("  },");
            writer_sprite.WriteLine("  \"swatchColours\": null,");
            writer_sprite.WriteLine("  \"swfPrecision\": 2.525,");
            writer_sprite.WriteLine("  \"textureGroupId\": {");
            writer_sprite.WriteLine("    \"name\": \"Default\",");
            writer_sprite.WriteLine("    \"path\": \"texturegroups/Default\",");
            writer_sprite.WriteLine("  },");
            writer_sprite.WriteLine("  \"type\": 0,");
            writer_sprite.WriteLine("  \"VTile\": false,");
            writer_sprite.WriteLine("  \"width\": " + tileset.Texture.SourceWidth + ",");
            writer_sprite.Write("}");

        }

        // END Create Sprite Tileset
        writer.WriteLine("  \"spriteId\": {");
        writer.WriteLine("    \"name\": \"spr_" + tileset.Name.Content + "\",");
        writer.WriteLine(
            "    \"path\": \"sprites/spr_"
                + tileset.Name.Content
                + "/spr_"
                + tileset.Name.Content
                + ".yy\","
        );
        writer.WriteLine("  },");
        writer.WriteLine("  \"spriteNoExport\": true,");
        writer.WriteLine("  \"textureGroupId\": {");
        writer.WriteLine(
            "    \"name\": \"" + tileset.Texture.TexturePage.TextureInfo.Name.Content + "\","
        );
        writer.WriteLine(
            "    \"path\": \"texturegroups/"
                + tileset.Texture.TexturePage.TextureInfo.Name.Content
                + "\","
        );
        writer.WriteLine("  },");
        writer.WriteLine("  \"tile_count\": " + tileset.GMS2TileIds.Count + ",");
        writer.WriteLine("  \"tileAnimation\": {");
        writer.WriteLine("    \"FrameData\": [");
        foreach (var tile_id in tileset.GMS2TileIds)
        {
            writer.WriteLine("      " + tile_id.ID + ",");
        }
        writer.WriteLine("    ],");
        writer.WriteLine("    \"SerialiseFrameCount\": 1,");
        writer.WriteLine("  },");
        writer.WriteLine("  \"tileAnimationFrames\": [],");
        writer.WriteLine("  \"tileAnimationSpeed\": 15.0,");
        writer.WriteLine("  \"tileHeight\": " + tileset.GMS2TileHeight + ",");
        writer.WriteLine("  \"tilehsep\": 0,");
        writer.WriteLine("  \"tilevsep\": 0,");
        writer.WriteLine("  \"tileWidth\": " + tileset.GMS2TileWidth + ",");
        writer.WriteLine("  \"tilexoff\": 0,");
        writer.WriteLine("  \"tileyoff\": 0,");
        writer.WriteLine("}");
    }

    IncrementProgressParallel();
}
