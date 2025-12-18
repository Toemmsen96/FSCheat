using System;
using System.IO;
using CTDynamicModMenu.Commands;
using MiniJSON;

namespace FSCheat.Cheats
{
    internal class EncryptGameSaves : CustomCommand
    {
        public override string Name => "Encrypt Game Saves";

        public override string Description => "Encrypts the game from the decrypted save files to make them usable again.";

        public override string Format => "/encryptsaves <file_path>";
        public override string Category => "Saves";
    // Use the OS local application data folder (e.g. C:\Users\<User>\AppData\Local on Windows)
    private static readonly string savesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FalloutShelter");
    // base name for decrypted output files (DecryptGameSaves writes: decrypted_Vault_1.json, etc.)
    private static readonly string defaultInputBase = Path.Combine(savesFolder, "decrypted_Vault");


        public override void Execute(CommandInput message)
        {
            try
            {
                if (message.Args.Count < 1)
                {
                    Utils.DisplayMessage("Using default input base: " + defaultInputBase + "{n}.json");
                }

                // Determine save files
                if (!Directory.Exists(savesFolder))
                {
                    Utils.DisplayError("Save folder not found: " + savesFolder);
                    return;
                }

                var saveFiles = Directory.GetFiles(savesFolder, "*.sav");
                if (saveFiles.Length == 0)
                {
                    Utils.DisplayError("No save (.sav) files found in: " + savesFolder);
                    return;
                }

                // Determine input files based on argument
                string argPath = message.Args.Count > 0 ? Environment.ExpandEnvironmentVariables(message.Args[0]) : null;
                string[] inputFiles = new string[0];

                if (!string.IsNullOrEmpty(argPath))
                {
                    if (Directory.Exists(argPath))
                    {
                        inputFiles = Directory.GetFiles(argPath);
                    }
                    else if (File.Exists(argPath))
                    {
                        inputFiles = new[] { argPath };
                    }
                    else
                    {
                        // Treat as base pattern: base + _{index}.json
                        var tmp = new System.Collections.Generic.List<string>();
                        for (int i = 1; i <= saveFiles.Length; i++)
                        {
                            var candidate = argPath + i + ".json";
                            if (File.Exists(candidate)) tmp.Add(candidate);
                        }
                        inputFiles = tmp.ToArray();
                    }
                }

                // If no arg or no matches, fallback to default base pattern
                if (inputFiles.Length == 0)
                {
                    var tmp = new System.Collections.Generic.List<string>();
                    for (int i = 1; i <= saveFiles.Length; i++)
                    {
                        var candidate = defaultInputBase + i + ".json";
                        if (File.Exists(candidate)) tmp.Add(candidate);
                    }
                    inputFiles = tmp.ToArray();
                }

                if (inputFiles.Length == 0)
                {
                    Utils.DisplayError("No input decrypted files found. Provide a file, a directory, or use the default decrypted_save_{n}.json files in: " + savesFolder);
                    return;
                }

                // Backup existing .sav files first into savesFolder/backup/<timestamp>
                try
                {
                    var backupDir = savesFolder + "\\backups\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "\\";
                    Directory.CreateDirectory(backupDir);
                    foreach (var f in saveFiles)
                    {
                        try
                        {
                            var name = Path.GetFileName(f);
                            var dest = Path.Combine(backupDir, name);
                            File.Copy(f, dest, true);
                        }
                        catch (Exception exCopy)
                        {
                            Utils.DisplayError("Warning: failed to backup " + f + " -> " + exCopy.Message);
                        }
                    }
                    Utils.DisplayMessage("Backed up existing .sav files to: " + backupDir);
                }
                catch (Exception ex)
                {
                    Utils.DisplayError("Warning: could not create backup directory: " + ex.Message);
                    return;
                }

                // Map input files to output save files by index
                int successCount = 0;
                for (int i = 0; i < inputFiles.Length; i++)
                {
                    string inputForThis = inputFiles[i];
                    
                    if (!File.Exists(inputForThis))
                    {
                        Utils.DisplayError($"Input file does not exist: {inputForThis}, skipping.");
                        continue;
                    }

                    Utils.DisplayMessage("Encrypting from input: " + inputForThis);
                    
                    string inputText;
                    try
                    {
                        inputText = File.ReadAllText(inputForThis);
                    }
                    catch (Exception exRead)
                    {
                        Utils.DisplayError($"Failed to read {inputForThis}: {exRead.Message}");
                        continue;
                    }

                    // If input is pretty JSON, re-serialize compact JSON to match original encrypted format
                    string compact;
                    try
                    {
                        var parsed = Json.Deserialize(inputText);
                        if (parsed != null)
                        {
                            compact = Json.Serialize(parsed);
                        }
                        else
                        {
                            compact = inputText;
                        }
                    }
                    catch
                    {
                        compact = inputText;
                    }

                    // Encrypt using the game's StringCipher.Encrypt method
                    string encryptedData;
                    try
                    {
                        if (string.IsNullOrEmpty(Patches.decryptPassphrase))
                        {
                            Utils.DisplayError("✗ Passphrase not captured yet. Load a save in-game first before encrypting.");
                            continue;
                        }
                        encryptedData = StringCipher.Encrypt(compact, Patches.decryptPassphrase);
                    }
                    catch (Exception exEncrypt)
                    {
                        Utils.DisplayError($"✗ Failed to encrypt {inputForThis}: {exEncrypt.Message}");
                        continue;
                    }
                    
                    // write to Vault{n}.sav using 1-based index
                    var outPath = Path.Combine(savesFolder, $"Vault{i + 1}.sav");
                    
                    try
                    {
                        File.WriteAllText(outPath, encryptedData);
                        Utils.DisplayMessage($"✓ Written encrypted save to: {outPath}");
                        successCount++;
                    }
                    catch (Exception exWrite)
                    {
                        Utils.DisplayError($"✗ Failed to write {outPath}: {exWrite.Message}");
                    }
                }

                Utils.DisplayMessage($"Encryption complete: {successCount} file(s) encrypted successfully.");
            }
            catch (Exception e)
            {
                Utils.DisplayError("Error: " + e.Message);
            }
            
        }
    }
}