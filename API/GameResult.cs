using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

struct GameResultField
{
    public string Id;
    public short DataType;
    public byte[] Data;
}

class GameResult
{
    private List<GameResultField> fields = new List<GameResultField>();

    public GameResult() { }

    public event EventHandler<CnCNetEventGameResultSuccess> OnGameResultComplete;
    public event EventHandler<CnCNetEventGameResultError> OnGameResultFailed;

    public GameResult(byte[] data)
    {
        if (data.Length % 4 != 0) throw new ArgumentException("invalid game result data");
        ParseFields(data);
    }

    public GameResult(string filePath)
    {
        byte[] data = File.ReadAllBytes(filePath);
        if (data.Length % 4 != 0) throw new Exception(string.Format("invalid game result data ({0})", filePath));
        ParseFields(data);
    }

    public void AddField(string id, object value)
    {
        if (id.Length != 4) throw new ArgumentException("id length does not equal 4");
        var field = new GameResultField();

        if (value is bool)
        {
            field.DataType = 2;
            field.Data = BitConverter.GetBytes((bool)value);
        }
        else if (value is int)
        {
            field.DataType = 5;
            field.Data = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((int)value));
        }
        else if (value is string)
        {
            field.DataType = 7;
            field.Data = Encoding.Default.GetBytes((string)value + "\0");
        }
        else throw new Exception("data type not supported yet");

        field.Id = id;
        AddField(field);
    }

    public void AddField(GameResultField field)
    {
        if (field.Data == null || field.Id == null) throw new ArgumentNullException();
        if (field.Id.Length != 4) throw new ArgumentException("id length does not equal 4");

        if (fields.FindIndex(f => f.Id == field.Id) > -1)
            throw new Exception(string.Format("Field already exists ({0})", field.Id));

        fields.Add(field);
    }

    public bool RemoveField(GameResultField field)
    {
        return RemoveField(field.Id);
    }

    public bool RemoveField(string id)
    {
        for (int i = 0; i < fields.Count; i++)
        {
            if (fields[i].Id == id)
            {
                fields.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public GameResultField GetField(string id)
    {
        for (int i = 0; i < fields.Count; i++)
        {
            if (fields[i].Id == id) return fields[i];
        }
        throw new Exception(string.Format("Field not found ({0})", id));
    }

    public List<GameResultField> GetAllFields()
    {
        return new List<GameResultField>(fields);
    }

    public byte[] GetBytes()
    {
        var result = new List<byte>();
        for (int i = 0; i < fields.Count; i++)
        {
            result.AddRange(Encoding.Default.GetBytes(fields[i].Id));
            result.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(fields[i].DataType)));
            result.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)fields[i].Data.Length)));
            result.AddRange(fields[i].Data);
            while (result.Count % 4 != 0) result.Add(0);
        }
        result.InsertRange(0, new byte[2]);
        result.InsertRange(0, BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)(result.Count + 2))));
        return result.ToArray();
    }

    public void SaveToFile(string filePath)
    {
        File.WriteAllBytes(filePath, GetBytes());
    }

    // TODO - Add Events & Tidy 
    public async Task<string> UploadToServer(string address, string accessToken)
    {
        try
        {
            var wc = new WebClient();

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer:" + accessToken);

            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new ByteArrayContent(GetBytes(), 0, GetBytes().Length), "file", "stats.dmp");

            HttpResponseMessage response = await httpClient.PostAsync(address, form);

            if (response.ReasonPhrase.Length > 0)
            {
                OnGameResultFailed?.Invoke(this, new CnCNetEventGameResultError(response.ReasonPhrase));
            }

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();

            OnGameResultComplete?.Invoke(this, new CnCNetEventGameResultSuccess(response.Content.ReadAsStringAsync().Result));
            return response.Content.ReadAsStringAsync().Result;
        }
        catch(Exception ex)
        {
            Console.WriteLine("EX: " + ex.Message);
            OnGameResultFailed?.Invoke(this, new CnCNetEventGameResultError("Uncaught exception"));
            return null;
        }
    }

    private void ParseFields(byte[] data)
    {
        using (var memoryStream = new MemoryStream(data))
        using (var binaryReader = new BinaryReader(memoryStream))
        {
            if (binaryReader.BaseStream.Length >= 4) binaryReader.BaseStream.Position = 4;
            while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                var field = new GameResultField();
                field.Id = Encoding.Default.GetString(binaryReader.ReadBytes(4));
                field.DataType = IPAddress.HostToNetworkOrder(binaryReader.ReadInt16());
                short length = IPAddress.HostToNetworkOrder(binaryReader.ReadInt16());
                field.Data = binaryReader.ReadBytes(length);
                while (binaryReader.BaseStream.Position % 4 != 0) binaryReader.BaseStream.Position++;
                fields.Add(field);
            }
        }
    }
}
