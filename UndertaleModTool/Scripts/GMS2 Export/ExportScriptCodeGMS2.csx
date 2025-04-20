using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

EnsureDataLoaded();

string codeFolder = Path.Combine(Path.GetDirectoryName(FilePath), "scripts");

// Supprimer le dossier s’il existe déjà
if (Directory.Exists(codeFolder))
{
    Directory.Delete(codeFolder, true);
}

Directory.CreateDirectory(codeFolder);

GlobalDecompileContext globalDecompileContext = new(Data);
Underanalyzer.Decompiler.IDecompileSettings decompilerSettings = Data.ToolInfo.DecompilerSettings;

List<UndertaleCode> toDump = Data.Code.Where(c => c.ParentEntry is null).ToList();

SetProgressBar(null, "Code Entries", 0, toDump.Count);
StartProgressBarUpdater();

await DumpCode();

await StopProgressBarUpdater();
HideProgressBar();
ScriptMessage("Export Complete.\n\nLocation: " + codeFolder);

async Task DumpCode()
{
    await Task.Run(() => Parallel.ForEach(toDump, DumpCode));
}

void DumpCode(UndertaleCode code)
{
    if (code is not null)
    {
        Console.Out.WriteLine(code.Name.Content);
        if (
            !(
                code.Name.Content.StartsWith("gml_Object_")
                || code.Name.Content.StartsWith("gml_Room_")
            )
        )
        {
            // string strippedName = code.Name.Content.Substring("gml_GlobalScript_".Length);
            string strippedName = code.Name.Content;
            strippedName = strippedName.Replace("gml_GlobalScript_", "");
            strippedName = strippedName.Replace("gml_Script_", "");
            Console.Out.WriteLine(strippedName);
            //
            string path = Path.Combine(codeFolder, strippedName);
            Directory.CreateDirectory(path);
            try
            {
                // Export .gml
                File.WriteAllText(
                    Path.Combine(path, strippedName + ".gml"),
                    (
                        code != null
                            ? new Underanalyzer.Decompiler.DecompileContext(
                                globalDecompileContext,
                                code,
                                decompilerSettings
                            ).DecompileToString()
                            : ""
                    )
                );
                // Export .yy
                using (
                    StreamWriter writer = new StreamWriter(Path.Combine(path, strippedName + ".yy"))
                )
                {
                    writer.WriteLine("{");
                    writer.WriteLine("  \"resourceType\": \"GMScript\",");
                    writer.WriteLine("  \"resourceVersion\": \"1.0\",");
                    writer.WriteLine("  \"name\": \"" + strippedName + "\",");
                    writer.WriteLine("  \"isCompatibility\": false,");
                    writer.WriteLine("  \"isDnD\": false,");
                    writer.WriteLine("  \"parent\": {");
                    writer.WriteLine("    \"name\": \"Scripts\",");
                    writer.WriteLine("    \"path\": \"folders/Scripts.yy\",");
                    writer.WriteLine("  },");
                    writer.Write("}");
                }
            }
            catch (Exception e)
            {
                File.WriteAllText(path, "/*\nDECOMPILER FAILED!\n\n" + e.ToString() + "\n*/");
            }
        }
    }

    IncrementProgressParallel();
}
