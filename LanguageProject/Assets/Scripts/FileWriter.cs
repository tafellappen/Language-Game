using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public static class FileWriter
{
    public static void WriteData(List<int> friendValues) {
        StreamWriter writer = null;
        try {
            writer = new StreamWriter(Application.dataPath + "/LonelySpace Data.txt", true);
            writer.WriteLine("\n" + DateTime.Now);
            for(int i = 0; i < friendValues.Count; i++) {
                writer.Write(friendValues[i] + ", ");

                if((i+1) % 5 == 0) {
                    writer.Write('\n');
                }
            }
        }
        catch {}
        finally {
            if(writer != null) {
                writer.Close();
            }
        }
    }
}
