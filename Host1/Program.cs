﻿using Orleans.Runtime.Host;
using Orleans;
using System.Net;
using System;
using GrainInterfaces;

class Program
{

    static SiloHost siloHost;

    static void Main(string[] args)
    {
        // Orleans should run in its own AppDomain, we set it up like this
        AppDomain hostDomain = AppDomain.CreateDomain("OrleansHost", null,
            new AppDomainSetup()
            {
                AppDomainInitializer = InitSilo
            });


        DoSomeClientWork();

        Console.WriteLine("Orleans Silo is running.\nPress Enter to terminate...");
        Console.ReadLine();

        // We do a clean shutdown in the other AppDomain
        hostDomain.DoCallBack(ShutdownSilo);
    }


    static void DoSomeClientWork()
    {
        // Orleans comes with a rich XML and programmatic configuration. Here we're just going to set up with basic programmatic config
        var config = Orleans.Runtime.Configuration.ClientConfiguration.LocalhostSilo(30000);
        GrainClient.Initialize(config);

        var friend = GrainClient.GrainFactory.GetGrain<IHello>(0);
        var result = friend.SayHello("Goodbye").Result;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(result);

        Console.WriteLine(friend.SayHelloInReverse("AaBbCc").Result);

        Console.ResetColor();

    }

    static void InitSilo(string[] args)
    {
        //http://dotnet.github.io/orleans/1.5/Tutorials/Running-in-a-Stand-alone-Silo.html

        siloHost = new SiloHost(System.Net.Dns.GetHostName());
        // The Cluster config is quirky and weird to configure in code, so we're going to use a config file
        siloHost.ConfigFileName = "OrleansConfiguration.xml";

        siloHost.InitializeOrleansSilo();
        var startedok = siloHost.StartOrleansSilo();
        if (!startedok)
            throw new SystemException(String.Format("Failed to start Orleans silo '{0}' as a {1} node", siloHost.Name, siloHost.Type));

    }

    static void ShutdownSilo()
    {
        if (siloHost != null)
        {
            siloHost.Dispose();
            GC.SuppressFinalize(siloHost);
            siloHost = null;
        }
    }

}