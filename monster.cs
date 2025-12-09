using System;
using System.Collections.Generic;

namespace Assembler
{
    public class SymbolAnalyzer
    {
        public Dictionary<string, int> CreateSymbolsTable(string[] instructionsWithLabels,
            out string[] instructionsWithoutLabels)
        {
            var symbolsTable = new Dictionary<string, int>
            {
                { "SP", 0 },
                { "LCL", 1 },
                { "ARG", 2 },
                { "THIS", 3 },
                { "THAT", 4 },
                
                { "R0", 0 },
                { "R1", 1 },
                { "R2", 2 },
                { "R3", 3 },
                { "R4", 4 },
                { "R5", 5 },
                { "R6", 6 },
                { "R7", 7 },
                { "R8", 8 },
                { "R9", 9 },
                { "R10", 10 },
                { "R11", 11 },
                { "R12", 12 },
                { "R13", 13 },
                { "R14", 14 },
                { "R15", 15 },
                
                { "SCREEN", 0x4000 },
                { "KBD", 0x6000 }
            };
            
            var instructionsList = new List<string>();
            
            int instructionAddress = 0;
            
            foreach (string instruction in instructionsWithLabels)
            {
                if (instruction.StartsWith("(") && instruction.EndsWith(")"))
                {
                    string labelName = instruction.Substring(1, instruction.Length - 2);
                    
                    if (!symbolsTable.ContainsKey(labelName))
                    {
                        symbolsTable[labelName] = instructionAddress;
                    }
                }
                else
                {
                    instructionsList.Add(instruction);
                    
                    instructionAddress++;
                }
            }
            
            instructionsWithoutLabels = instructionsList.ToArray();
            
            return symbolsTable;
        }
    }
}
