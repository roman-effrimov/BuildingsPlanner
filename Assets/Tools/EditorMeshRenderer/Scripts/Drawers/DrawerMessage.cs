using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invisibles
{
    public class DrawerMessage
    {
        public MessageType type;
        public string text;

        public void SetErrorMessage(string text)
        {
            if(type != MessageType.error)
            {
                this.text = text;
                type = MessageType.error;
            }
        }

        public void SetWarningMessage(string text)
        {
            if(type == MessageType.none)
            {
                this.text = text;
                type = MessageType.warning;
            }
        }

        public void ClearError()
        {
            if (type == MessageType.error)
            {
                text = string.Empty;
                type = MessageType.none;
            }
        }

        public void ClearWarning()
        {
            if (type == MessageType.warning)
            {
                text = string.Empty;
                type = MessageType.none;
            }
        }

        public void Clear()
        {
            if(type != MessageType.none)
            {
                text = string.Empty;
                type = MessageType.none;
            }
        }

        public enum MessageType
        {
            none,
            warning,
            error
        }
    }
}