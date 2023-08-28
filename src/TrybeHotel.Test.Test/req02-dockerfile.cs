namespace trybe_hotel.Test.Test;
using System.Diagnostics;
using System.IO;

public class TestReq02
{
    [Fact]
    [Trait("Category", "2. Desenvolva o Dockerfile")]
    public static void Testes()
    {

        ProcessStartInfo startInfo = new ProcessStartInfo()
        {
            FileName = System.Environment.CurrentDirectory.ToString().Replace("bin/Debug/net6.0","") + "dockertest.sh",
            WorkingDirectory = System.Environment.CurrentDirectory.ToString().Replace("bin/Debug/net6.0",""),
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };
        Process proc = new Process()
        {
            StartInfo = startInfo
        };
        proc.Start();
        proc.WaitForExit();

        string saidaConsole = "";
        
        StreamReader sr = new StreamReader("../../../dockerlog.txt");
        string line = sr.ReadLine();
        while (line != null)
        {        
            line = sr.ReadLine();
            saidaConsole += line;
        }
        sr.Close();
        Assert.Contains("listening on", saidaConsole);

    }
}

/*
docker build --tag trybehotel:teste1 ../TrybeHotel/

docker images

docker run --name trybehotel_teste1 -d trybehotel:teste1

docker logs trybehotel_teste1

docker rm -f trybehotel_teste1

docker image rm --force trybehotel:teste1
*/
