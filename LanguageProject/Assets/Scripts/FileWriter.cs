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
            string path = Application.dataPath + "/LonelySpaceData.txt";
            Debug.Log(path);
            writer = new StreamWriter(path, true);
            writer.WriteLine("\n" + DateTime.Now);
            for(int i = 0; i < friendValues.Count; i++) {
                writer.Write(friendValues[i] + ", ");

                if((i+1) % 5 == 0) {
                    writer.Write('\n');
                }
            }
        }
        catch (Exception e)
        {
            //Debug.Log(e.ToString());
        }
        finally {
            if(writer != null) {
                writer.Close();
            }
        }
    }
}
