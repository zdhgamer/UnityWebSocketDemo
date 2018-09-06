using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class CommonUtil {

    /// <summary>
    /// 计算字符串的md5
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string GetMD5ForString(string source) {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] data = Encoding.UTF8.GetBytes(source);
        byte[] md5Data = md5.ComputeHash(data, 0, data.Length);
        md5.Clear();
        StringBuilder builder = new StringBuilder();
        for (int i=0;i<md5Data.Length;i++) {
            builder.Append(Convert.ToString(md5Data[i], 16).PadLeft(2, '0'));
        }
        string result = builder.ToString();
        result = result.PadLeft(32, '0');
        return result;
    }

    /// <summary>
    /// 计算文件的md5值
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetMD5ByFile(string path) {
        using (FileStream fs = new FileStream(path, FileMode.Open)) {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] data = md5.ComputeHash(fs);
            StringBuilder builder = new StringBuilder();
            for (int i=0;i<data.Length;i++) {
                builder.Append(data[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

}
