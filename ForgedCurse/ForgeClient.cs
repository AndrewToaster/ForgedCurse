﻿using ForgedCurse.Sections;
using ForgedCurse.Utility;
using System;
using System.Net.Http;
using System.Text.Json;

namespace ForgedCurse
{
    /// <summary>
    /// Main class for interacting with the CurseForge API
    /// </summary>
    /// <remarks>
    /// Documentation taken from 'https://twitchappapi.docs.apiary.io/'
    /// </remarks>
    public sealed class ForgeClient : IDisposable
    {
        private const string API_URL = "https://addons-ecs.forgesvc.net/api/v2/";

        public readonly HttpClient HttpClient;

        public AddonSection Addons { get; }
        public FileSection Files { get; }
        public MincraftSection Minecraft { get; }

        /// <summary>
        /// The default <see cref="Utility.RetryPolicy"/> to use during any communication with the CurseForge API
        /// </summary>
        public static RetryPolicy RetryPolicy { get; set; }

        internal static JsonSerializerOptions SerializerSettings { get; }

        static ForgeClient()
        {
            RetryPolicy = new(5, 5000, ex => throw ex);
            SerializerSettings = new()
            {
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>
        /// Constructs a new <see cref="ForgeClient"/> instance
        /// </summary>
        public ForgeClient()
        {
            HttpClient = new()
            {
                BaseAddress = new Uri(API_URL)
            };
            Addons = new(this);
            Files = new(this);
            Minecraft = new(this);
        }

        /// <summary>
        /// Releases all allocated resources and disposes all <see cref="IDisposable"/>s instantiated
        /// </summary>
        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }
}
