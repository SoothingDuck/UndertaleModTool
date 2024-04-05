// Made by mono21400

using System.Text;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UndertaleModLib.Util;
using System.Linq;
using System.Windows.Forms;

EnsureDataLoaded();

string fntPaths = GetFolder(FilePath) + "Export_Paths" + Path.DirectorySeparatorChar;
TextureWorker worker = new TextureWorker();
Directory.CreateDirectory(fntPaths);

SetProgressBar(null, "Paths", 0, Data.Paths.Count);
StartProgressBarUpdater();

await DumpPaths();
worker.Cleanup();

await StopProgressBarUpdater();
HideProgressBar();
ScriptMessage("Export Complete.\n\nLocation: " + fntPaths);


string GetFolder(string path)
{
    return Path.GetDirectoryName(path) + Path.DirectorySeparatorChar;
}

async Task DumpPaths()
{
    await Task.Run(() => Parallel.ForEach(Data.Paths, DumpPath));
}

void DumpPath(UndertalePath path)
{
    //if (arrayString.Contains(font.Name.ToString().Replace("\"", "")))
    //{
    //worker.ExportAsPNG(path.Texture, fntPaths + path.Name.Content + ".png");
    using (StreamWriter writer = new StreamWriter(fntPaths + path.Name.Content + ".path.gmx"))
    {
        writer.WriteLine("<!--This Document is generated by GameMaker, if you edit it by hand then you do so at your own risk!-->");
        writer.WriteLine("<path>");
        if(path.IsSmooth)
        {
            writer.WriteLine("  <kind>-1</kind>");
        } 
        else
        {
            writer.WriteLine("  <kind>0</kind>");
        }
        if (path.IsClosed)
        {
            writer.WriteLine("  <closed>-1</closed>");
        }
        else
        {
            writer.WriteLine("  <closed>0</closed>");
        }
        writer.WriteLine("  <precision>"+ path.Precision +"</precision>");
        writer.WriteLine("  <backroom>-1</backroom>");
        writer.WriteLine("  <hsnap>16</hsnap>");
        writer.WriteLine("  <vsnap>16</vsnap>");
        writer.WriteLine("  <points>");
        foreach (var g in path.Points)
        {
            writer.WriteLine("    <point>"+ g.X +","+ g.Y +","+ g.Speed +"</point>");
        }
        writer.WriteLine("  </points>");

        //writer.WriteLine("  <name>" + path.Name.Content + "</name>");
        //writer.WriteLine("  <size>" + path.EmSize + "</size>");
        //if(path.Bold)
        //{
        //    writer.WriteLine("  <bold>-1</bold>");
        //} else
        //{
        //    writer.WriteLine("  <bold>0</bold>");
        //}
        //writer.WriteLine("  <renderhq>0</renderhq>");
        //if (path.Italic)
        //{
        //    writer.WriteLine("  <italic>-1</italic>");
        //}
        //else
        //{
        //    writer.WriteLine("  <italic>0</italic>");
        //}
        //writer.WriteLine("  <charset>" + path.Charset + "</charset>");
        //writer.WriteLine("  <aa>" + path.AntiAliasing + "</aa>");
        //writer.WriteLine("  <includeTTF>0</includeTTF>");
        //writer.WriteLine("  <TTFName></TTFName>");
        //writer.WriteLine("  <texgroups>");
        //writer.WriteLine("    <texgroup0>0</texgroup0>");
        //writer.WriteLine("  </texgroups>");
        //writer.WriteLine("  <ranges>");
        //writer.WriteLine("    <range0>" + path.RangeStart + "," + path.RangeEnd +"</range0>");
        //writer.WriteLine("  </ranges>");

        //writer.WriteLine("  <glyphs>");
        //foreach (var g in path.Glyphs)
        //{
        //    writer.WriteLine("    <glyph character=\""+ g.Character + "\" x=\""+ g.SourceX + "\" y=\"" + g.SourceY + "\" w=\"" + g.SourceWidth + "\" h=\"" + g.SourceHeight + "\" shift=\"" + g.Shift + "\" offset=\"" + g.Offset + "\"/>");
        //}
        //writer.WriteLine("  </glyphs>");

        ////writer.WriteLine(font.DisplayName + ";" + font.EmSize + ";" + font.Bold + ";" + font.Italic + ";" + font.Charset + ";" + font.AntiAliasing + ";" + font.ScaleX + ";" + font.ScaleY);

        ////foreach (var g in font.Glyphs)
        ////{
        ////    writer.WriteLine(g.Character + ";" + g.SourceX + ";" + g.SourceY + ";" + g.SourceWidth + ";" + g.SourceHeight + ";" + g.Shift + ";" + g.Offset);
        ////}

        //writer.WriteLine("  <kerningPairs/>");
        //writer.WriteLine("  <image>" + path.Name.Content + ".png</image>");
        writer.WriteLine("</path>");
    }
    //}

    IncrementProgressParallel();
}