using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UndertaleModLib.Models;

EnsureDataLoaded();

// Pour avoir un "." au lieu d'une "," dans les conversion en décimal
System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)
    System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
customCulture.NumberFormat.NumberDecimalSeparator = ".";

System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

string sequencesFolder = GetFolder(FilePath) + "sequences" + Path.DirectorySeparatorChar;

if (Directory.Exists(sequencesFolder))
{
    Directory.Delete(sequencesFolder, true);
}

Directory.CreateDirectory(sequencesFolder);

bool exportFromCache = false;

// if (GMLCacheEnabled && Data.GMLCache is not null)
//     exportFromCache = ScriptQuestion("Export from the cache?");

List<UndertaleSequence> toDump;
if (!exportFromCache)
{
    toDump = new();
    if (Data.Sequences != null)
    {
        foreach (UndertaleSequence sequence in Data.Sequences)
        {
            toDump.Add(sequence);
        }
    }
}

SetProgressBar(null, "Sequence Entries", 0, toDump.Count);
StartProgressBarUpdater();

await DumpSequences();

await StopProgressBarUpdater();
HideProgressBar();
ScriptMessage("Export Complete.\n\nLocation: " + sequencesFolder);

string GetFolder(string path)
{
    return Path.GetDirectoryName(path) + Path.DirectorySeparatorChar;
}

async Task DumpSequences()
{
    // if (Data.KnownSubFunctions is null) //if we run script before opening any code
    //     Decompiler.BuildSubFunctionCache(Data);

    await Task.Run(() => Parallel.ForEach(toDump, DumpSequence));
}

void DumpTracks(StreamWriter writer, List<UndertaleSequence.Track> tracks, int tabnum = 2)
{
    String myspaces = new String(' ', tabnum * 2);

    foreach (var track in tracks)
    {
        switch (track.ModelName.Content)
        {
            case "GMGraphicTrack":
                // writer.WriteLine(track.ModelName.Content); // GMGraphicTrack
                // writer.WriteLine(track.Name.Content); // spr_arc_long
                // writer.WriteLine(track.BuiltinName); // 0
                // writer.WriteLine(track.Traits); // None
                // writer.WriteLine(track.IsCreationTrack); // False
                // writer.WriteLine(track.Tags.Count); // 0
                // writer.WriteLine(track.OwnedResources.Count); // 0
                // writer.WriteLine(track.Tracks.Count); // 5
                writer.WriteLine(
                    myspaces
                        + "{\"resourceType\":\"GMGraphicTrack\",\"resourceVersion\":\"1.0\",\"name\":\"spr_arc_long\",\"builtinName\":0,\"events\":[],\"inheritsTrackColour\":true,\"interpolation\":1,\"isCreationTrack\":false,\"keyframes\":{\"resourceType\":\"KeyframeStore<AssetSpriteKeyframe>\",\"resourceVersion\":\"1.0\",\"Keyframes\":["
                );
                // Keyframes
                writer.WriteLine(
                    myspaces + "],},\"modifiers\":[],\"trackColour\":4292102386,\"tracks\":["
                );
                // Tracks
                DumpTracks(writer, track.Tracks, tabnum + 1);
                writer.WriteLine(myspaces + "],\"traits\":0,}");
                break;

            case "GMInstanceTrack":
                // writer.WriteLine(track.ModelName.Content); // GMGraphicTrack
                // writer.WriteLine(track.Name.Content); // spr_arc_long
                // writer.WriteLine(track.BuiltinName); // 0
                // writer.WriteLine(track.Traits); // None
                // writer.WriteLine(track.IsCreationTrack); // False
                // writer.WriteLine(track.Tags.Count); // 0
                // writer.WriteLine(track.OwnedResources.Count); // 0
                // writer.WriteLine(track.Tracks.Count); // 5
                writer.WriteLine(
                    myspaces
                        + "{\"resourceType\":\"GMInstanceTrack\",\"resourceVersion\":\"1.0\",\"name\":\""
                        + track.Name.Content
                        + "\",\"builtinName\":"
                        + (int)track.BuiltinName
                        + ",\"events\":[],\"inheritsTrackColour\":true,\"interpolation\":1,\"isCreationTrack\":false,\"keyframes\":{\"resourceType\":\"KeyframeStore<AssetInstanceKeyframe>\",\"resourceVersion\":\"1.0\",\"Keyframes\":["
                );

                UndertaleSequence.InstanceKeyframes instanceKeyframes =
                    track.Keyframes as UndertaleSequence.InstanceKeyframes;
                foreach (var frame in instanceKeyframes.List)
                {
                    writer.WriteLine(
                        "          {\"resourceType\":\"Keyframe<AssetInstanceKeyframe>\",\"resourceVersion\":\"1.0\",\"Channels\":{\"0\":{\"resourceType\":\"AssetInstanceKeyframe\",\"resourceVersion\":\"1.0\",\"Id\":{\"name\":\"obj_game_over_button_retry\",\"path\":\"objects/obj_game_over_button_retry/obj_game_over_button_retry.yy\",},},},\"Disabled\":false,\"id\":\"7378af4f-4d7a-4666-8a98-54d6964cb41a\",\"IsCreationKey\":false,\"Key\":20.0,\"Length\":60.0,\"Stretch\":false,},"
                    );
                }
                writer.WriteLine(
                    "        ],},\"modifiers\":[],\"trackColour\":4283298376,\"tracks\":["
                );
                writer.WriteLine(
                    "        {\"resourceType\":\"GMColourTrack\",\"resourceVersion\":\"1.0\",\"name\":\"blend_multiply\",\"builtinName\":10,\"events\":[],\"inheritsTrackColour\":true,\"interpolation\":1,\"isCreationTrack\":false,\"keyframes\":{\"resourceType\":\"KeyframeStore<ColourKeyframe>\",\"resourceVersion\":\"1.0\",\"Keyframes\":["
                );
                writer.WriteLine(
                    "              {\"resourceType\":\"Keyframe<ColourKeyframe>\",\"resourceVersion\":\"1.0\",\"Channels\":{\"0\":{\"resourceType\":\"ColourKeyframe\",\"resourceVersion\":\"1.0\",\"AnimCurveId\":null,\"Colour\":16777215,\"EmbeddedAnimCurve\":null,},},\"Disabled\":false,\"id\":\"0431139e-476b-4c36-8c42-993ac8c39b23\",\"IsCreationKey\":false,\"Key\":20.0,\"Length\":1.0,\"Stretch\":false,},"
                );
                writer.WriteLine(
                    "              {\"resourceType\":\"Keyframe<ColourKeyframe>\",\"resourceVersion\":\"1.0\",\"Channels\":{\"0\":{\"resourceType\":\"ColourKeyframe\",\"resourceVersion\":\"1.0\",\"AnimCurveId\":null,\"Colour\":4294967295,\"EmbeddedAnimCurve\":null,},},\"Disabled\":false,\"id\":\"2451f299-a416-4fb8-8928-d7ffc1ff27c6\",\"IsCreationKey\":false,\"Key\":30.0,\"Length\":1.0,\"Stretch\":false,},"
                );
                writer.WriteLine(
                    "            ],},\"modifiers\":[],\"trackColour\":4283298376,\"tracks\":[],\"traits\":0,},"
                );
                writer.WriteLine(
                    "        {\"resourceType\":\"GMRealTrack\",\"resourceVersion\":\"1.0\",\"name\":\"origin\",\"builtinName\":16,\"events\":[],\"inheritsTrackColour\":true,\"interpolation\":1,\"isCreationTrack\":false,\"keyframes\":{\"resourceType\":\"KeyframeStore<RealKeyframe>\",\"resourceVersion\":\"1.0\",\"Keyframes\":["
                );
                writer.WriteLine(
                    "              {\"resourceType\":\"Keyframe<RealKeyframe>\",\"resourceVersion\":\"1.0\",\"Channels\":{\"0\":{\"resourceType\":\"RealKeyframe\",\"resourceVersion\":\"1.0\",\"AnimCurveId\":null,\"EmbeddedAnimCurve\":null,\"RealValue\":0.0,},\"1\":{\"resourceType\":\"RealKeyframe\",\"resourceVersion\":\"1.0\",\"AnimCurveId\":null,\"EmbeddedAnimCurve\":null,\"RealValue\":0.0,},},\"Disabled\":false,\"id\":\"5683c924-00a0-4338-a197-02b4c2aeefd3\",\"IsCreationKey\":false,\"Key\":20.0,\"Length\":1.0,\"Stretch\":false,},"
                );
                writer.WriteLine(
                    "              {\"resourceType\":\"Keyframe<RealKeyframe>\",\"resourceVersion\":\"1.0\",\"Channels\":{\"0\":{\"resourceType\":\"RealKeyframe\",\"resourceVersion\":\"1.0\",\"AnimCurveId\":null,\"EmbeddedAnimCurve\":null,\"RealValue\":0.0,},\"1\":{\"resourceType\":\"RealKeyframe\",\"resourceVersion\":\"1.0\",\"AnimCurveId\":null,\"EmbeddedAnimCurve\":null,\"RealValue\":0.0,},},\"Disabled\":false,\"id\":\"77cc84da-0aad-4b9d-9472-8cfecffc1579\",\"IsCreationKey\":false,\"Key\":30.0,\"Length\":1.0,\"Stretch\":false,},"
                );
                writer.WriteLine(
                    "            ],},\"modifiers\":[],\"trackColour\":4283298376,\"tracks\":[],\"traits\":0,},"
                );
                writer.WriteLine(
                    "        {\"resourceType\":\"GMRealTrack\",\"resourceVersion\":\"1.0\",\"name\":\"position\",\"builtinName\":14,\"events\":[],\"inheritsTrackColour\":true,\"interpolation\":1,\"isCreationTrack\":false,\"keyframes\":{\"resourceType\":\"KeyframeStore<RealKeyframe>\",\"resourceVersion\":\"1.0\",\"Keyframes\":["
                );
                writer.WriteLine(
                    "              {\"resourceType\":\"Keyframe<RealKeyframe>\",\"resourceVersion\":\"1.0\",\"Channels\":{\"0\":{\"resourceType\":\"RealKeyframe\",\"resourceVersion\":\"1.0\",\"AnimCurveId\":null,\"EmbeddedAnimCurve\":null,\"RealValue\":90.0,},\"1\":{\"resourceType\":\"RealKeyframe\",\"resourceVersion\":\"1.0\",\"AnimCurveId\":null,\"EmbeddedAnimCurve\":null,\"RealValue\":90.0,},},\"Disabled\":false,\"id\":\"a40b77e0-40b0-4c64-b337-dd14de16d755\",\"IsCreationKey\":false,\"Key\":20.0,\"Length\":1.0,\"Stretch\":false,},"
                );
                writer.WriteLine(
                    "              {\"resourceType\":\"Keyframe<RealKeyframe>\",\"resourceVersion\":\"1.0\",\"Channels\":{\"0\":{\"resourceType\":\"RealKeyframe\",\"resourceVersion\":\"1.0\",\"AnimCurveId\":null,\"EmbeddedAnimCurve\":null,\"RealValue\":90.0,},\"1\":{\"resourceType\":\"RealKeyframe\",\"resourceVersion\":\"1.0\",\"AnimCurveId\":null,\"EmbeddedAnimCurve\":null,\"RealValue\":90.0,},},\"Disabled\":false,\"id\":\"3be06cee-a6b4-42d9-b94f-198286c26e6a\",\"IsCreationKey\":false,\"Key\":30.0,\"Length\":1.0,\"Stretch\":false,},"
                );
                writer.WriteLine(
                    "            ],},\"modifiers\":[],\"trackColour\":4283298376,\"tracks\":[],\"traits\":0,},"
                );
                writer.WriteLine(
                    "        {\"resourceType\":\"GMRealTrack\",\"resourceVersion\":\"1.0\",\"name\":\"rotation\",\"builtinName\":8,\"events\":[],\"inheritsTrackColour\":true,\"interpolation\":1,\"isCreationTrack\":false,\"keyframes\":{\"resourceType\":\"KeyframeStore<RealKeyframe>\",\"resourceVersion\":\"1.0\",\"Keyframes\":["
                );
                writer.WriteLine(
                    "              {\"resourceType\":\"Keyframe<RealKeyframe>\",\"resourceVersion\":\"1.0\",\"Channels\":{\"0\":{\"resourceType\":\"RealKeyframe\",\"resourceVersion\":\"1.0\",\"AnimCurveId\":null,\"EmbeddedAnimCurve\":null,\"RealValue\":0.0,},},\"Disabled\":false,\"id\":\"7d10c438-d6d6-41f4-9f7c-99bad7a021a1\",\"IsCreationKey\":false,\"Key\":20.0,\"Length\":1.0,\"Stretch\":false,},"
                );
                writer.WriteLine(
                    "              {\"resourceType\":\"Keyframe<RealKeyframe>\",\"resourceVersion\":\"1.0\",\"Channels\":{\"0\":{\"resourceType\":\"RealKeyframe\",\"resourceVersion\":\"1.0\",\"AnimCurveId\":null,\"EmbeddedAnimCurve\":null,\"RealValue\":0.0,},},\"Disabled\":false,\"id\":\"e6f9ee5e-6fff-40ec-85b5-c2470271ae02\",\"IsCreationKey\":false,\"Key\":30.0,\"Length\":1.0,\"Stretch\":false,},"
                );
                writer.WriteLine(
                    "            ],},\"modifiers\":[],\"trackColour\":4283298376,\"tracks\":[],\"traits\":0,},"
                );
                writer.WriteLine(
                    "        {\"resourceType\":\"GMRealTrack\",\"resourceVersion\":\"1.0\",\"name\":\"scale\",\"builtinName\":15,\"events\":[],\"inheritsTrackColour\":true,\"interpolation\":1,\"isCreationTrack\":false,\"keyframes\":{\"resourceType\":\"KeyframeStore<RealKeyframe>\",\"resourceVersion\":\"1.0\",\"Keyframes\":["
                );
                writer.WriteLine(
                    "              {\"resourceType\":\"Keyframe<RealKeyframe>\",\"resourceVersion\":\"1.0\",\"Channels\":{\"0\":{\"resourceType\":\"RealKeyframe\",\"resourceVersion\":\"1.0\",\"AnimCurveId\":null,\"EmbeddedAnimCurve\":null,\"RealValue\":0.5,},\"1\":{\"resourceType\":\"RealKeyframe\",\"resourceVersion\":\"1.0\",\"AnimCurveId\":null,\"EmbeddedAnimCurve\":null,\"RealValue\":0.5,},},\"Disabled\":false,\"id\":\"2a5a3aec-eff2-48cc-9250-b00d8e95d9eb\",\"IsCreationKey\":false,\"Key\":20.0,\"Length\":1.0,\"Stretch\":false,},"
                );
                writer.WriteLine(
                    "              {\"resourceType\":\"Keyframe<RealKeyframe>\",\"resourceVersion\":\"1.0\",\"Channels\":{\"0\":{\"resourceType\":\"RealKeyframe\",\"resourceVersion\":\"1.0\",\"AnimCurveId\":null,\"EmbeddedAnimCurve\":null,\"RealValue\":1.0,},\"1\":{\"resourceType\":\"RealKeyframe\",\"resourceVersion\":\"1.0\",\"AnimCurveId\":null,\"EmbeddedAnimCurve\":null,\"RealValue\":1.0,},},\"Disabled\":false,\"id\":\"bd2b770c-88c1-416b-b9e1-6a41e0a4cef0\",\"IsCreationKey\":false,\"Key\":30.0,\"Length\":1.0,\"Stretch\":false,},"
                );
                writer.WriteLine(
                    "            ],},\"modifiers\":[],\"trackColour\":4283298376,\"tracks\":[],\"traits\":0,},"
                );
                writer.WriteLine("      ],\"traits\":0,},");

                writer.WriteLine(
                    myspaces
                        + "{\"resourceType\":\"GMInstanceTrack\",\"resourceVersion\":\"1.0\",\"name\":\"spr_arc_long\",\"builtinName\":0,\"events\":[],\"inheritsTrackColour\":true,\"interpolation\":1,\"isCreationTrack\":false,\"keyframes\":{\"resourceType\":\"KeyframeStore<AssetSpriteKeyframe>\",\"resourceVersion\":\"1.0\",\"Keyframes\":["
                );
                // Keyframes
                writer.WriteLine(
                    myspaces + "],},\"modifiers\":[],\"trackColour\":4292102386,\"tracks\":["
                );
                // Tracks
                DumpTracks(writer, track.Tracks, tabnum + 1);
                writer.WriteLine(myspaces + "],\"traits\":0,}");
                break;

            case "GMColourTrack":
                writer.WriteLine(track.ModelName.Content); // GMGraphicTrack
                writer.WriteLine(track.Name.Content); // spr_arc_long
                writer.WriteLine(track.BuiltinName); // 0
                writer.WriteLine(track.Traits); // None
                writer.WriteLine(track.IsCreationTrack); // False
                writer.WriteLine(track.Tags.Count); // 0
                writer.WriteLine(track.OwnedResources.Count); // 0
                writer.WriteLine(track.Tracks.Count); // 5
                writer.WriteLine(
                    myspaces
                        + "{\"resourceType\":\"GMColourTrack\",\"resourceVersion\":\"1.0\",\"name\":\"spr_arc_long\",\"builtinName\":0,\"events\":[],\"inheritsTrackColour\":true,\"interpolation\":1,\"isCreationTrack\":false,\"keyframes\":{\"resourceType\":\"KeyframeStore<AssetSpriteKeyframe>\",\"resourceVersion\":\"1.0\",\"Keyframes\":["
                );
                // Keyframes
                writer.WriteLine(
                    myspaces + "],},\"modifiers\":[],\"trackColour\":4292102386,\"tracks\":["
                );
                // Tracks
                DumpTracks(writer, track.Tracks, tabnum + 1);
                writer.WriteLine(myspaces + "],\"traits\":0,}");
                break;

            case "GMRealTrack":
                writer.WriteLine(track.ModelName.Content); // GMGraphicTrack
                writer.WriteLine(track.Name.Content); // spr_arc_long
                writer.WriteLine(track.BuiltinName); // 0
                writer.WriteLine(track.Traits); // None
                writer.WriteLine(track.IsCreationTrack); // False
                writer.WriteLine(track.Tags.Count); // 0
                writer.WriteLine(track.OwnedResources.Count); // 0
                writer.WriteLine(track.Tracks.Count); // 5
                writer.WriteLine(
                    myspaces
                        + "{\"resourceType\":\"GMRealTrack\",\"resourceVersion\":\"1.0\",\"name\":\"spr_arc_long\",\"builtinName\":0,\"events\":[],\"inheritsTrackColour\":true,\"interpolation\":1,\"isCreationTrack\":false,\"keyframes\":{\"resourceType\":\"KeyframeStore<AssetSpriteKeyframe>\",\"resourceVersion\":\"1.0\",\"Keyframes\":["
                );
                // Keyframes
                writer.WriteLine(
                    myspaces + "],},\"modifiers\":[],\"trackColour\":4292102386,\"tracks\":["
                );
                // Tracks
                DumpTracks(writer, track.Tracks, tabnum + 1);
                writer.WriteLine(myspaces + "],\"traits\":0,}");
                break;

            default:
                throw new Exception("Non traité : " + track.ModelName.Content);
                break;
        }
    }
}

void DumpSequence(UndertaleSequence sequence)
{
    Directory.CreateDirectory(sequencesFolder + sequence.Name.Content);

    using (
        StreamWriter writer = new StreamWriter(
            sequencesFolder
                + sequence.Name.Content
                + Path.DirectorySeparatorChar
                + sequence.Name.Content
                + ".yy"
        )
    )
    {
        writer.WriteLine("{");
        writer.WriteLine("  \"resourceType\": \"GMSequence\",");
        writer.WriteLine("  \"resourceVersion\": \"1.4\",");
        writer.WriteLine("  \"name\": \"" + sequence.Name.Content + "\",");
        writer.WriteLine("  \"autoRecord\": true,");
        writer.WriteLine("  \"backdropHeight\": 1080,");
        writer.WriteLine("  \"backdropImageOpacity\": 0.5,");
        writer.WriteLine("  \"backdropImagePath\": \"\",");
        writer.WriteLine("  \"backdropWidth\": 1920,");
        writer.WriteLine("  \"backdropXOffset\": 0.0,");
        writer.WriteLine("  \"backdropYOffset\": 0.0,");
        writer.WriteLine("  \"events\": {");
        writer.WriteLine("    \"resourceType\": \"KeyframeStore<MessageEventKeyframe>\",");
        writer.WriteLine("    \"resourceVersion\": \"1.0\",");
        writer.WriteLine("    \"Keyframes\": [],");
        writer.WriteLine("  },");
        writer.WriteLine("  \"eventStubScript\": null,");
        writer.WriteLine("  \"eventToFunction\": {},");
        writer.WriteLine("  \"length\": " + sequence.Length.ToString("0.0") + ",");
        writer.WriteLine("  \"lockOrigin\": false,");
        writer.WriteLine("  \"moments\": {");
        writer.WriteLine("    \"resourceType\": \"KeyframeStore<MomentsEventKeyframe>\",");
        writer.WriteLine("    \"resourceVersion\": \"1.0\",");
        writer.WriteLine("    \"Keyframes\": [],");
        writer.WriteLine("  },");
        writer.WriteLine("  \"parent\": {");
        writer.WriteLine("    \"name\": \"Sequences\",");
        writer.WriteLine("    \"path\": \"folders/Sequences.yy\",");
        writer.WriteLine("  },");
        writer.WriteLine("  \"playback\": " + (int)sequence.Playback + ",");
        writer.WriteLine("  \"playbackSpeed\": " + sequence.PlaybackSpeed.ToString("0.0") + ",");
        writer.WriteLine("  \"playbackSpeedType\": " + (int)sequence.PlaybackSpeedType + ",");
        writer.WriteLine("  \"showBackdrop\": true,");
        writer.WriteLine("  \"showBackdropImage\": false,");
        writer.WriteLine("  \"spriteId\": null,");
        writer.WriteLine("  \"timeUnits\": 1,");
        writer.WriteLine("  \"tracks\": [");

        DumpTracks(writer, new List<UndertaleSequence.Track>(sequence.Tracks));

        writer.WriteLine("  ],");
        writer.WriteLine("  \"visibleRange\": null,");
        writer.WriteLine("  \"volume\": 1.0,");
        writer.WriteLine("  \"xorigin\": " + sequence.OriginX + ",");
        writer.WriteLine("  \"yorigin\": " + sequence.OriginY + ",");
        writer.Write("}");
    }

    IncrementProgressParallel();
}
void DumpCachedCode(KeyValuePair<string, string> code)
{
    string path = Path.Combine(sequencesFolder, code.Key + ".gml");

    File.WriteAllText(path, code.Value);

    IncrementProgressParallel();
}
