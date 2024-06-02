using System;

namespace GameWorldClassLibrary.Models
{
    [Serializable]
    public class Player
    {
        private Guid id;
        private string name;
        private string? ip;
        private int? port;

        public string Name { get => name; set => name = value; }
        public Guid Id { get => id; set => id = value; }
        public string? Ip { get => ip; set => ip = value; }
        public int? Port { get => port; set => port = value; }

        public Player(string name, string? ip, int? port)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.ip = ip;
            this.port = port;
        }

        public Player(Guid id, string name, string? ip, int? port)
        {
            this.id = id;
            this.name = name;
            this.ip = ip;
            this.port = port;
        }

        public Player()
        {
            this.id = Guid.Empty;
            this.name = string.Empty;
            this.ip = string.Empty;
            this.port = 0;
        }

        public Player(string name)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.ip = string.Empty;
            this.port = 0;
        }

        public Player(Guid id, string name)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.ip = string.Empty;
            this.port = 0;
        }

        public override string ToString()
        {
            return this.name;
        }

        public (int nrParameters, object[] parameters) Play(IGame newGame)
        {
            throw new NotImplementedException();
        }

        public static Player Null()
        {
            return new Player(Guid.Empty, "Null", "Null", 0);
        }

        public static Player Bot()
        {
            return new Player(Guid.Empty, "Bot", "Null", 0);
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Player p = (Player)obj;
            return this.id == p.Id;
        }
    }
}