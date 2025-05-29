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

void DumpTracks(StreamWriter writer, List<UndertaleSequence.Track> tracks, int myspace = 4)
{
    String myspaces = new String(' ', myspace);

    foreach (var track in tracks)
    {
        switch (track.ModelName.Content)
        {
            case "GMInstanceTrack":
                writer.WriteLine(
                    myspaces
                        + "{\"resourceType\":\"GMInstanceTrack\",\"resourceVersion\":\"1.0\",\"name\":\""
                        + track.Name.Content
                        + "\",\"builtinName\":"
                        + (int)track.BuiltinName
                        + ",\"events\":[],\"inheritsTrackColour\":true,\"interpolation\":1,\"isCreationTrack\":false,\"keyframes\":{\"resourceType\":\"KeyframeStore<AssetInstanceKeyframe>\",\"resourceVersion\":\"1.0\",\"Keyframes\":["
                );
                // Keyframes
                UndertaleSequence.InstanceKeyframes instanceKeyframes =
                    track.Keyframes as UndertaleSequence.InstanceKeyframes;
                foreach (var frame in instanceKeyframes.List)
                {
                    writer.Write(
                        myspaces
                            + "      "
                            + "{\"resourceType\":\"Keyframe<AssetInstanceKeyframe>\",\"resourceVersion\":\"1.0\",\"Channels\":{"
                    );
                    // Channels
                    foreach (
                        KeyValuePair<
                            int,
                            UndertaleSequence.InstanceKeyframes.Data
                        > kvp in frame.Channels
                    )
                    {
                        writer.Write(
                            "\""
                                + kvp.Key
                                + "\":{\"resourceType\":\"AssetInstanceKeyframe\",\"resourceVersion\":\"1.0\",\"Id\":{\"name\":\""
                                + kvp.Value.Resource.Resource.Name.Content
                                + "\",\"path\":\"objects/"
                                + kvp.Value.Resource.Resource.Name.Content
                                + "/"
                                + kvp.Value.Resource.Resource.Name.Content
                                + ".yy\",},},"
                        );
                    }
                    // End Channels
                    writer.WriteLine(
                        "},\"Disabled\":"
                            + (frame.Disabled ? "true" : "false")
                            + ",\"IsCreationKey\":false,\"Key\":"
                            + frame.Key.ToString("0.0")
                            + ",\"Length\":"
                            + frame.Length.ToString("0.0")
                            + ",\"Stretch\":"
                            + (frame.Stretch ? "true" : "false")
                            + ",},"
                    );
                }
                // End Keyframes
                writer.Write(
                    myspaces + "    ],},\"modifiers\":[],\"trackColour\":4283298376,\"tracks\":["
                );
                // Tracks
                if (track.Tracks.Count > 0)
                {
                    writer.WriteLine("");
                    DumpTracks(writer, track.Tracks, myspace + 4);
                    writer.Write(new String(' ', myspace + 2));
                }
                writer.WriteLine("],\"traits\":0,},");
                break;

            case "GMGraphicTrack":
                writer.WriteLine(
                    myspaces
                        + "{\"resourceType\":\"GMGraphicTrack\",\"resourceVersion\":\"1.0\",\"name\":\""
                        + track.Name.Content
                        + "\",\"builtinName\":"
                        + (int)track.BuiltinName
                        + ",\"events\":[],\"inheritsTrackColour\":true,\"interpolation\":1,\"isCreationTrack\":false,\"keyframes\":{\"resourceType\":\"KeyframeStore<ColourKeyframe>\",\"resourceVersion\":\"1.0\",\"Keyframes\":["
                );
                // Keyframes
                UndertaleSequence.GraphicKeyframes graphicKeyframes =
                    track.Keyframes as UndertaleSequence.GraphicKeyframes;
                foreach (var frame in graphicKeyframes.List)
                {
                    writer.Write(
                        myspaces
                            + "      "
                            + "{\"resourceType\":\"Keyframe<ColourKeyframe>\",\"resourceVersion\":\"1.0\",\"Channels\":{"
                    );
                    // Channels
                    foreach (
                        KeyValuePair<
                            int,
                            UndertaleSequence.GraphicKeyframes.Data
                        > kvp in frame.Channels
                    )
                    {
                        throw new Exception("Non traité");
                    }
                    // End Channels
                    writer.WriteLine(
                        "},\"Disabled\":"
                            + (frame.Disabled ? "true" : "false")
                            + ",\"IsCreationKey\":false,\"Key\":"
                            + frame.Key.ToString("0.0")
                            + ",\"Length\":"
                            + frame.Length.ToString("0.0")
                            + ",\"Stretch\":"
                            + (frame.Stretch ? "true" : "false")
                            + ",},"
                    );
                }
                // End Keyframes
                writer.Write(
                    myspaces + "    ],},\"modifiers\":[],\"trackColour\":4283298376,\"tracks\":["
                );
                // Tracks
                if (track.Tracks.Count > 0)
                {
                    writer.WriteLine("");
                    DumpTracks(writer, track.Tracks, myspace + 4);
                }
                writer.WriteLine("],\"traits\":0,},");
                break;

            case "GMColourTrack":
                writer.WriteLine(
                    myspaces
                        + "{\"resourceType\":\"GMColourTrack\",\"resourceVersion\":\"1.0\",\"name\":\""
                        + track.Name.Content
                        + "\",\"builtinName\":"
                        + (int)track.BuiltinName
                        + ",\"events\":[],\"inheritsTrackColour\":true,\"interpolation\":1,\"isCreationTrack\":false,\"keyframes\":{\"resourceType\":\"KeyframeStore<ColourKeyframe>\",\"resourceVersion\":\"1.0\",\"Keyframes\":["
                );
                // Keyframes
                UndertaleSequence.RealKeyframes colourKeyframes =
                    track.Keyframes as UndertaleSequence.RealKeyframes;
                foreach (var frame in colourKeyframes.List)
                {
                    writer.Write(
                        myspaces
                            + "      "
                            + "{\"resourceType\":\"Keyframe<ColourKeyframe>\",\"resourceVersion\":\"1.0\",\"Channels\":{"
                    );
                    // Channels
                    foreach (KeyValuePair<int, UndertaleSequence.RealData> kvp in frame.Channels)
                    {
                        writer.Write(
                            "\""
                                // + kvp.Key
                                + "4294967295"
                                + "\":{\"resourceType\":\"ColourKeyframe\",\"resourceVersion\":\"1.0\",\"AnimCurveId\":null,\"Colour\":"
                                + (int)kvp.Value.Value
                                + ",\"EmbeddedAnimCurve\":null,},"
                        );
                    }
                    // End Channels
                    writer.WriteLine(
                        "},\"Disabled\":"
                            + (frame.Disabled ? "true" : "false")
                            + ",\"IsCreationKey\":false,\"Key\":"
                            + frame.Key.ToString("0.0")
                            + ",\"Length\":"
                            + frame.Length.ToString("0.0")
                            + ",\"Stretch\":"
                            + (frame.Stretch ? "true" : "false")
                            + ",},"
                    );
                }
                // End Keyframes
                writer.Write(
                    myspaces + "    ],},\"modifiers\":[],\"trackColour\":4283298376,\"tracks\":["
                );
                // Tracks
                if (track.Tracks.Count > 0)
                {
                    writer.WriteLine("");
                    DumpTracks(writer, track.Tracks, myspace + 4);
                }
                writer.WriteLine("],\"traits\":0,},");
                break;

            case "GMRealTrack":
                writer.WriteLine(
                    myspaces
                        + "{\"resourceType\":\"GMRealTrack\",\"resourceVersion\":\"1.0\",\"name\":\""
                        + track.Name.Content
                        + "\",\"builtinName\":"
                        + (int)track.BuiltinName
                        + ",\"events\":[],\"inheritsTrackColour\":true,\"interpolation\":1,\"isCreationTrack\":false,\"keyframes\":{\"resourceType\":\"KeyframeStore<RealKeyframe>\",\"resourceVersion\":\"1.0\",\"Keyframes\":["
                );
                // Keyframes
                UndertaleSequence.RealKeyframes realKeyframes =
                    track.Keyframes as UndertaleSequence.RealKeyframes;
                foreach (var frame in realKeyframes.List)
                {
                    writer.Write(
                        myspaces
                            + "      "
                            + "{\"resourceType\":\"Keyframe<RealKeyframe>\",\"resourceVersion\":\"1.0\",\"Channels\":{"
                    );
                    // Channels
                    foreach (KeyValuePair<int, UndertaleSequence.RealData> kvp in frame.Channels)
                    {
                        writer.Write(
                            "\""
                                + kvp.Key
                                + "\":{\"resourceType\":\"RealKeyframe\",\"resourceVersion\":\"1.0\",\"AnimCurveId\":null,\"EmbeddedAnimCurve\":null,\"RealValue\":"
                                + kvp.Value.Value.ToString("0.0")
                                + ",},"
                        );
                    }
                    // End Channels
                    writer.WriteLine(
                        "},\"Disabled\":"
                            + (frame.Disabled ? "true" : "false")
                            + ",\"IsCreationKey\":false,\"Key\":"
                            + frame.Key.ToString("0.0")
                            + ",\"Length\":"
                            + frame.Length.ToString("0.0")
                            + ",\"Stretch\":"
                            + (frame.Stretch ? "true" : "false")
                            + ",},"
                    );
                }
                // End Keyframes
                writer.Write(
                    myspaces + "    ],},\"modifiers\":[],\"trackColour\":4283298376,\"tracks\":["
                );
                // Tracks
                if (track.Tracks.Count > 0)
                {
                    writer.WriteLine("");
                    DumpTracks(writer, track.Tracks, myspace + 4);
                }
                writer.WriteLine("],\"traits\":0,},");
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
