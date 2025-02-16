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
        writer.WriteLine("  \"out_columns\": 2,");
        writer.WriteLine("  \"out_tilehborder\": 2,");
        writer.WriteLine("  \"out_tilevborder\": 2,");
        writer.WriteLine("  \"parent\": {");
        writer.WriteLine("    \"name\": \"Tile Sets\",");
        writer.WriteLine("    \"path\": \"folders/Tile Sets.yy\",");
        writer.WriteLine("  },");
        // TODO Create Sprite Tileset

        // Un peu de travail avec Récupération du Script

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
