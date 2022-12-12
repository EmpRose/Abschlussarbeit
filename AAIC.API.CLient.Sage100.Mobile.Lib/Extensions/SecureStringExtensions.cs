using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.Extensions
{
    public static class SecureStringExtensions
    {
        // Konvertiert eine normale Zeichenkette in eine "Nur-Lesen"-Zeichenkette (SecureString)
        public static SecureString ToSecureString(this string source)           // schließt ähnliche Namen von "source" aus und bearbeitet direkt diese Variable/Zeichenkette
        {
            if (string.IsNullOrWhiteSpace(source))                              // wenn  Zeichenkette "source" nichts beinhaltet oder nur Leerzeichen hat, gebe "nichts" zurück.
            {
                return null;
            }
            var secureString = new SecureString();                              // erstelle Variable "secureString" als Objekt der Klasse "SecureString"
            foreach (var character in source)                                   // für jedes Zeichen in der Zeichenkette wird ein Zeichen in die Zeichenkette angehängt
            {
                secureString.AppendChar(character);
            }
            secureString.MakeReadOnly();                                        // die Zeichenkette "secureString" ist ab jetzt "nur lesbar"
            return secureString;                                                // gebe die bearbeitete und nur lesbare Zeichenkette "secureString" zurück
        }

        // Konvertiert aus einer "sicheren" Zeichentekke eine normale Zeichenkette
        public static string FromSecureString(this SecureString source)
        {
            if (source == null)                                                 // wenn in der Quelle-SecureString "null" ist, gebe nichts (null) zurück
            {
                return null;
            }
            var handle = IntPtr.Zero;                                           // deklariert die Variable "handle" mit dem Inhalt "IntPtr.Zero" (ein nicht initialisierter Zeiger)
            try
            {
                handle = Marshal.SecureStringToGlobalAllocUnicode(source);      // Kopiert den Inhalt von "Quelle" in einen nicht initialisierten Zeiger


                return Marshal.PtrToStringUni(handle);                          // Gibt die Variable "handle" zurück mit allen ab dem Index "0" von einem nicht gemenagedem Inhalt in einer Zeichenkette
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(handle);                     // Zeigt die Adresse der nicht verwalteten Zeichenkette, die freigegeben werden soll an.
            }
        }
    }
}
