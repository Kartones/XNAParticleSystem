using System;

namespace ExampleParticleEngine
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ParticleEngineGame game = new ParticleEngineGame())
            {
                game.Run();
            }
        }
    }
}

