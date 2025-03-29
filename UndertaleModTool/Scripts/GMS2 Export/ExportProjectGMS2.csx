using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UndertaleModLib.Util;

EnsureDataLoaded();

string rootFolder = Path.GetDirectoryName(FilePath) + Path.DirectorySeparatorChar;

// Export Project yyp
using (
    StreamWriter writer = new StreamWriter(rootFolder + Data.GeneralInfo.FileName.Content + ".yyp")
)
{
    writer.WriteLine("{");
    writer.WriteLine("  \"resourceType\": \"GMProject\",");
    writer.WriteLine("  \"resourceVersion\": \"1.7\",");
    writer.WriteLine("  \"name\": \"" + Data.GeneralInfo.Name.Content + "\",");
    writer.WriteLine("  \"AudioGroups\": [");
    writer.WriteLine(
        "    {\"resourceType\":\"GMAudioGroup\",\"resourceVersion\":\"1.3\",\"name\":\"audiogroup_default\",\"targets\":-1,},"
    );
    writer.WriteLine("  ],");
    writer.WriteLine("  \"configs\": {");
    writer.WriteLine("    \"children\": [],");
    writer.WriteLine("    \"name\": \"Default\",");
    writer.WriteLine("  },");
    writer.WriteLine("  \"defaultScriptType\": 0,");
    writer.WriteLine("  \"Folders\": [");
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Fonts\",\"folderPath\":\"folders/Fonts.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Notes\",\"folderPath\":\"folders/Notes.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Objects\",\"folderPath\":\"folders/Objects.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Board and Pieces\",\"folderPath\":\"folders/Objects/Board and Pieces.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Controllers\",\"folderPath\":\"folders/Objects/Controllers.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Effects\",\"folderPath\":\"folders/Objects/Effects.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"HUD\",\"folderPath\":\"folders/Objects/HUD.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Main Menu\",\"folderPath\":\"folders/Objects/Main Menu.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Rooms\",\"folderPath\":\"folders/Rooms.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Scripts\",\"folderPath\":\"folders/Scripts.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Sequences\",\"folderPath\":\"folders/Sequences.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Sounds\",\"folderPath\":\"folders/Sounds.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Music\",\"folderPath\":\"folders/Sounds/Music.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"SFX\",\"folderPath\":\"folders/Sounds/SFX.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Sprites\",\"folderPath\":\"folders/Sprites.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"BackgroundsAndTiles\",\"folderPath\":\"folders/Sprites/BackgroundsAndTiles.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"HUD\",\"folderPath\":\"folders/Sprites/HUD.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Pieces\",\"folderPath\":\"folders/Sprites/Pieces.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Tile Sets\",\"folderPath\":\"folders/Tile Sets.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Paths\",\"folderPath\":\"folders/Paths.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Shaders\",\"folderPath\":\"folders/Shaders.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Timelines\",\"folderPath\":\"folders/Timelines.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Animation Curves\",\"folderPath\":\"folders/Animation Curves.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Extensions\",\"folderPath\":\"folders/Extensions.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Particle Systems\",\"folderPath\":\"folders/Particle Systems.yy\",},"
    );
    writer.WriteLine("  ],");
    writer.WriteLine("  \"IncludedFiles\": [],");
    writer.WriteLine("  \"isEcma\": false,");
    writer.WriteLine("  \"LibraryEmitters\": [],");
    writer.WriteLine("  \"MetaData\": {");
    writer.WriteLine("    \"IDEVersion\": \"2023.11.1.129\",");
    writer.WriteLine("  },");
    writer.WriteLine("  \"resources\": [");
    // Sprites
    for (int i = 0; i < Data.Sprites.Count; i++)
    {
        UndertaleSprite sprite = Data.Sprites[i];
        writer.WriteLine(
            "    {\"id\":{\"name\":\""
                + sprite.Name.Content
                + "\",\"path\":\"sprites/"
                + sprite.Name.Content
                + "/"
                + sprite.Name.Content
                + ".yy\",},},"
        );
    }
    // Tilesets
    for (int i = 0; i < Data.Backgrounds.Count; i++)
    {
        UndertaleBackground tileset = Data.Backgrounds[i];
        writer.WriteLine(
            "    {\"id\":{\"name\":\"spr_"
                + tileset.Name.Content
                + "\",\"path\":\"sprites/spr_"
                + tileset.Name.Content
                + "/spr_"
                + tileset.Name.Content
                + ".yy\",},},"
        );
    }
    // Game Objects
    for (int i = 0; i < Data.GameObjects.Count; i++)
    {
        UndertaleGameObject game_object = Data.GameObjects[i];
        writer.WriteLine(
            "    {\"id\":{\"name\":\""
                + game_object.Name.Content
                + "\",\"path\":\"objects/"
                + game_object.Name.Content
                + "/"
                + game_object.Name.Content
                + ".yy\",},},"
        );
    }
    // Scripts
    for (int i = 0; i < Data.Scripts.Count; i++)
    {
        UndertaleScript script = Data.Scripts[i];
        if (!(script.Name.Content.StartsWith("gml_")))
        {
            writer.WriteLine(
                "    {\"id\":{\"name\":\""
                    + script.Name.Content
                    + "\",\"path\":\"scripts/"
                    + script.Name.Content
                    + "/"
                    + script.Name.Content
                    + ".yy\",},},"
            );
        }
    }
    // Sounds
    for (int i = 0; i < Data.Sounds.Count; i++)
    {
        UndertaleSound sound = Data.Sounds[i];
        writer.WriteLine(
            "    {\"id\":{\"name\":\""
                + sound.Name.Content
                + "\",\"path\":\"sounds/"
                + sound.Name.Content
                + "/"
                + sound.Name.Content
                + ".yy\",},},"
        );
    }
    // Fonts
    for (int i = 0; i < Data.Fonts.Count; i++)
    {
        UndertaleFont font = Data.Fonts[i];
        writer.WriteLine(
            "    {\"id\":{\"name\":\""
                + font.Name.Content
                + "\",\"path\":\"fonts/"
                + font.Name.Content
                + "/"
                + font.Name.Content
                + ".yy\",},},"
        );
    }
    writer.WriteLine(
        "    {\"id\":{\"name\":\"tileset_board\",\"path\":\"tilesets/tileset_board/tileset_board.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"seq_game_over_win_template\",\"path\":\"sequences/seq_game_over_win_template/seq_game_over_win_template.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"seq_no_valid_matches\",\"path\":\"sequences/seq_no_valid_matches/seq_no_valid_matches.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"Documentation\",\"path\":\"notes/Documentation/Documentation.yy\",},},"
    );
    for (int i = 0; i < Data.Rooms.Count; i++)
    {
        UndertaleRoom room = Data.Rooms[i];
        writer.WriteLine(
            "    {\"id\":{\"name\":\""
                + room.Name.Content
                + "\",\"path\":\"rooms/"
                + room.Name.Content
                + "/"
                + room.Name.Content
                + ".yy\",},},"
        );
    }
    writer.WriteLine(
        "    {\"id\":{\"name\":\"seq_pause\",\"path\":\"sequences/seq_pause/seq_pause.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"seq_game_over_lose\",\"path\":\"sequences/seq_game_over_lose/seq_game_over_lose.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"seq_game_over_win_level\",\"path\":\"sequences/seq_game_over_win_level/seq_game_over_win_level.yy\",},},"
    );
    writer.WriteLine("  ],");
    writer.WriteLine("  \"RoomOrderNodes\": [");
    foreach (var resource in Data.GeneralInfo.RoomOrder)
    {
        UndertaleRoom room = resource.Resource;
        writer.WriteLine(
            "    {\"roomId\":{\"name\":\""
                + room.Name.Content
                + "\",\"path\":\"rooms/"
                + room.Name.Content
                + "/"
                + room.Name.Content
                + ".yy\",},},"
        );
    }
    writer.WriteLine("  ],");
    writer.WriteLine("  \"templateType\": \"game\",");
    writer.WriteLine("  \"TextureGroups\": [");
    writer.WriteLine(
        "    {\"resourceType\":\"GMTextureGroup\",\"resourceVersion\":\"1.3\",\"name\":\"Default\",\"autocrop\":true,\"border\":2,\"compressFormat\":\"bz2\",\"directory\":\"\",\"groupParent\":null,\"isScaled\":true,\"loadType\":\"default\",\"mipsToGenerate\":0,\"targets\":-1,},"
    );
    writer.WriteLine("  ],");
    writer.Write("}");
}

// Export Project Resource Order
using (
    StreamWriter writer = new StreamWriter(
        rootFolder + Data.GeneralInfo.FileName.Content + ".resource_order"
    )
)
{
    writer.WriteLine("{");
    writer.WriteLine("  \"FolderOrderSettings\": [");
    writer.WriteLine("    {\"name\":\"Paths\",\"order\":1,\"path\":\"folders/Paths.yy\",},");
    writer.WriteLine("    {\"name\":\"Shaders\",\"order\":2,\"path\":\"folders/Shaders.yy\",},");
    writer.WriteLine(
        "    {\"name\":\"Timelines\",\"order\":3,\"path\":\"folders/Timelines.yy\",},"
    );
    writer.WriteLine(
        "    {\"name\":\"Animation Curves\",\"order\":4,\"path\":\"folders/Animation Curves.yy\",},"
    );
    writer.WriteLine(
        "    {\"name\":\"Extensions\",\"order\":5,\"path\":\"folders/Extensions.yy\",},"
    );
    writer.WriteLine(
        "    {\"name\":\"Particle Systems\",\"order\":6,\"path\":\"folders/Particle Systems.yy\",},"
    );
    writer.WriteLine("  ],");
    writer.WriteLine("  \"ResourceOrderSettings\": [");
    writer.WriteLine(
        "    {\"name\":\"seq_game_over_win_template\",\"order\":4,\"path\":\"sequences/seq_game_over_win_template/seq_game_over_win_template.yy\",},"
    );
    writer.WriteLine(
        "    {\"name\":\"seq_no_valid_matches\",\"order\":1,\"path\":\"sequences/seq_no_valid_matches/seq_no_valid_matches.yy\",},"
    );
    writer.WriteLine(
        "    {\"name\":\"seq_game_over_lose\",\"order\":2,\"path\":\"sequences/seq_game_over_lose/seq_game_over_lose.yy\",},"
    );
    writer.WriteLine(
        "    {\"name\":\"seq_game_over_win_level\",\"order\":3,\"path\":\"sequences/seq_game_over_win_level/seq_game_over_win_level.yy\",},"
    );
    writer.WriteLine("  ],");
    writer.Write("}");
}

ScriptMessage("Export Complete.\n\nLocation: " + rootFolder);
