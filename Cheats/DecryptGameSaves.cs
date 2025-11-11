

using System;
using System.IO;
using CTDynamicModMenu.Commands;
using Microsoft.Win32;
using MiniJSON;

namespace FSCheat.Cheats
{
    internal class DecryptGameSaves : CustomCommand
    {
        public override string Name => "Decrypt Game Saves";

        public override string Description => "Decrypts the game saves and saves it to a file.";

        public override string Format => "/decryptsaves <file_path>";
        public override string Category => "Saves";

    // Use the OS local application data folder (e.g. C:\Users\<User>\AppData\Local on Windows)
    private static readonly string savesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FalloutShelter");
    private static readonly string defaultFilePath = Path.Combine(savesFolder, "decrypted_Vault");

        public override void Execute(CommandInput message)
        {
            try
            {
                if (message.Args.Count < 1)
                {
                    Utils.DisplayMessage("Using default file path: " + defaultFilePath);
                }

                // Allow the user to pass a specific path; expand any environment variables they include
                string filePath = message.Args.Count > 0 ? Environment.ExpandEnvironmentVariables(message.Args[0]) : defaultFilePath;

                // Ensure the save folder exists
                if (!Directory.Exists(savesFolder))
                {
                    Utils.DisplayMessage("Save folder not found: " + savesFolder);
                    return;
                }

                var saveFiles = Directory.GetFiles(savesFolder, "*.sav");
                if (saveFiles.Length == 0)
                {
                    Utils.DisplayMessage("No save (.sav) files found in: " + savesFolder);
                    return;
                }

                // Ensure the output directory exists
                var outDir = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(outDir))
                {
                    Directory.CreateDirectory(outDir);
                }

                int fileIndex = 1;
                foreach (var saveFile in saveFiles)
                {
                    Utils.DisplayMessage("Decrypting save file: " + saveFile);
                    var decryptedData = StringCipher.Decrypt(File.ReadAllText(saveFile), Patches.decryptPassphrase);

                    // If decrypted text is JSON, parse and pretty-print it; otherwise write raw text.
                    string outputText = decryptedData;
                    try
                    {
                        var parsed = Json.Deserialize(decryptedData);
                        if (parsed != null)
                        {
                            outputText = PrettyPrintJson(parsed);
                        }
                    }
                    catch
                    {
                        // parsing failed, leave outputText as raw decrypted data
                        outputText = decryptedData;
                    }

                    // Write the (possibly pretty-printed) output to the requested file. This will overwrite if multiple saves are present.
                    File.WriteAllText(filePath + fileIndex + ".json", outputText);
                    fileIndex++;
                }

                Utils.DisplayMessage("Game saves decrypted and saved to " + filePath);
            }
            catch (Exception e)
            {
                Utils.DisplayMessage("Error: " + e.Message);
            }

            
        }

        // Pretty-print an object parsed by MiniJSON into nicely indented JSON
        private static string PrettyPrintJson(object obj)
        {
            var sb = new System.Text.StringBuilder();

            void WriteValue(object value, int indent)
            {
                if (value is System.Collections.IDictionary dict)
                {
                    var keys = new System.Collections.ArrayList(dict.Keys);
                    sb.AppendLine("{");
                    for (int i = 0; i < keys.Count; i++)
                    {
                        var key = keys[i];
                        var val = dict[key];
                        sb.Append(new string(' ', (indent + 1) * 2));
                        sb.Append('"');
                        sb.Append(key.ToString());
                        sb.Append("\": ");
                        WriteValue(val, indent + 1);
                        if (i < keys.Count - 1) sb.Append(',');
                        sb.AppendLine();
                    }
                    sb.Append(new string(' ', indent * 2));
                    sb.Append('}');
                }
                else if (value is System.Collections.IList list)
                {
                    sb.AppendLine("[");
                    for (int i = 0; i < list.Count; i++)
                    {
                        sb.Append(new string(' ', (indent + 1) * 2));
                        WriteValue(list[i], indent + 1);
                        if (i < list.Count - 1) sb.Append(',');
                        sb.AppendLine();
                    }
                    sb.Append(new string(' ', indent * 2));
                    sb.Append(']');
                }
                else
                {
                    // Primitive types: use MiniJSON serializer to get valid JSON representation
                    sb.Append(Json.Serialize(value));
                }
            }

            WriteValue(obj, 0);
            return sb.ToString();
        }
    }
}