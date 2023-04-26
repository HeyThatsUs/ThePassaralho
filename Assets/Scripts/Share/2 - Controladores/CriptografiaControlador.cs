using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class CriptografiaControlador
{
    private static byte[] ObterChave()
    {
        return new byte[]{ 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
    }

    private static byte[] ObterValor()
    {   
        return new byte[] { 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F };
    }

    public static byte[] Criptografar(string Texto)
    {
        var key = ObterChave();
        var iv = ObterValor();

        using (var provider = new AesCryptoServiceProvider())
        using (var encryptor = provider.CreateEncryptor(key, iv))
        using (var stream = new MemoryStream())
        using (var cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(Texto);
            cryptoStream.Write(inputBytes, 0, inputBytes.Length);
            cryptoStream.FlushFinalBlock();
            return stream.ToArray(); 
        }
    }

    public static string Descriptografar(byte[] input)
    {
        var key = ObterChave();
        var iv = ObterValor();

        using (var provider = new AesCryptoServiceProvider())
        using (var decryptor = provider.CreateDecryptor(key, iv))
        using (var stream = new MemoryStream(input))
        using (var cryptoStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read))
        {
            byte[] outputBytes = new byte[input.Length];
            int bytesRead = cryptoStream.Read(outputBytes, 0, outputBytes.Length);
            return Encoding.UTF8.GetString(outputBytes, 0, bytesRead);
        }
    }
}