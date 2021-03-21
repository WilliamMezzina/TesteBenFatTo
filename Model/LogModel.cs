using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteBenFatoo.Model
{
    [Serializable]
    public class LogModel
    {
        private int _id;
        private string _ip;
        private string _user;
        private DateTime? _horaLog;
        private string _comando;
        private string _site;
        private string _protocolo;
        private string _previousState;
        private string _actualState;
        private string _destino;
        private string _userAgent;

        public int Id { get => _id; set => _id = value; }
        public string Ip { get => _ip; set => _ip = value; }
        public string User { get => _user; set => _user = value; }
        public DateTime? HoraLog { get => _horaLog; set => _horaLog = value; }
        public string Comando { get => _comando; set => _comando = value; }
        public string Site { get => _site; set => _site = value; }
        public string Protocolo { get => _protocolo; set => _protocolo = value; }
        public string PreviousState { get => _previousState; set => _previousState = value; }
        public string ActualState { get => _actualState; set => _actualState = value; }
        public string Destino { get => _destino; set => _destino = value; }
        public string UserAgent { get => _userAgent; set => _userAgent = value; }
    }
}