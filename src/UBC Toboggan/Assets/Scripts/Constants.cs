using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants 
{
    public class Prompts 
    {
        public static string PauseMenuTitlePrompt = "Paused";
        public static string PauseMenuControlsTabPrompt = "Controls";
        public static string PauseMenuAudioTabPrompt = "Audio";
        public static string PauseMenuMusicSliderPrompt = "Music";
        public static string PauseMenuEffectsSliderPrompt = "Effects";
        public static string PauseMenuControllerControlsPrompt = "\n<pos=5%>The sled will naturally slide down the hill due\n<pos=5%>to gravity.\n<size=120%><pos=5%>On the Ground <pos=50%>In the Air</size>\n<pos=5%><size=200%><voffset=0.4em><sprite=\"Cross\" anim=\"0,3,8\"></voffset></size>   Jump <pos=50%><size=200%><voffset=0.2em><sprite=\"Left_Stick\" index=\"0\"></voffset></size>Rotate Clockwise\n<pos=5%><size=200%><voffset=0.1em><sprite=\"Square\" anim=\"0,3,8\"></voffset></size>Boost <pos=50%><size=200%><voffset=0.1em><sprite=\"Left_Stick\" index=\"2\"></voffset></size>Rotate AntiClockwise";

        public static string PauseMenuControllerLeftTabPrompt;
        public static string PauseMenuControllerRightTabPrompt;
        public static string PauseMenuControllerExitPrompt = "<voffset=-0.1em><sprite=\"Option\" anim=\"0,3,8\"></voffset> to return to game, <voffset=-0.1em><sprite=\"Cross\" anim=\"0,3,8\"></voffset> to restart,\nand <voffset=-0.1em><sprite=\"Circle\" anim=\"0,3,8\"></voffset> to quit game";

        public static string PauseMenuControllerAudioNavPrompt;
        public static string PauseMenuKeyboardControlsPrompt = "\n<pos=5%>The sled will naturally slide down the hill due\n<pos=5%>to gravity.\n<size=120%><pos=5%>On the Ground <pos=50%>In the Air</size>\n<pos=5%><size=200%><voffset=-0.2em><sprite=\"W\" anim=\"0,2,6\"></voffset></size> Jump <pos=50%><size=200%><voffset=-0.2em><sprite=\"D\" anim=\"0,2,6\"></voffset></size> Rotate Clockwise\n<pos=5%><size=200%><voffset=-0.2em><sprite=\"SPACE\" anim=\"0,2,6\"></voffset></size> Boost <pos=50%><size=200%><voffset=-0.2em><sprite=\"A\" anim=\"0,2,6\"></voffset></size> Rotate AntiClockwise";
        public static string PauseMenuKeyboardLeftTabPrompt;
        public static string PauseMenuKeyboardRightTabPrompt;
        public static string PauseMenuKeyboardExitPrompt = "voffset=-0.1em><sprite=\"ENTER\" anim=\"0,2,6\"></voffset> to return to game, <voffset=-0.1em><sprite=\"R\" anim=\"0,2,6\"></voffset> to restart,\nand <voffset=-0.1em><sprite=\"Q\" anim=\"0,2,6\"></voffset> to quit game";
        public static string PauseMenuKeyboardAudioNavPrompt;

        public static string MainMenuWelcomePrompt = "Welcome";
        public static string MainMenuControlsPrompt = "How to Play";
        public static string MainMenuControllerStartPrompt = "Press  <voffset=0.55em><sprite=\"Cross\" anim=\"0,3,8\"></voffset> to start";
        public static string MainMenuControllerQuitPrompt = "Press  <voffset=0.4em><sprite=\"Circle\" anim=\"0,3,8\"></voffset>to quit";
        public static string MainMenuControllerControlsPrompt = string.Format("{0}\n<pos=5%><size=200%><voffset=0.1em><sprite=\"Option\" anim=\"0,3,8\"></voffset></size> Pause", PauseMenuControllerControlsPrompt);
        public static string MainMenuControllerSkipPrompt = "Press  <voffset=0.5em><sprite=\"Cross\" anim=\"0,3,8\"></voffset> to skip";
        public static string MainMenuKeyboardStartPrompt = "Press <sprite=\"ENTER\" anim=\"0,2,6\"> to start";
        public static string MainMenuKeyboardQuitPrompt = "Press <sprite=\"Q\" anim=\"0,2,6\"> to quit";
        public static string MainMenuKeyboardControlsPrompt =  string.Format("{0}<pos=5%><size=200%><voffset=-0.2em><sprite=\"ENTER\" anim=\"0,2,6\"></voffset></size> Pause", PauseMenuKeyboardControlsPrompt);
        public static string MainMenuKeyboardSkipPrompt = "Press <sprite=\"ENTER\" anim=\"0,2,6\"> to skip";

        public static string GameOverTitlePrompt = "Game Over";
        public static string GameOverControllerExitPrompt = "<voffset=0.5em><sprite=\"Cross\" anim=\"0,3,8\"></voffset> to restart and  <voffset=0.1em><sprite=\"Circle\" anim=\"0,3,8\"></voffset>to quit game";

        public static string GameOverKeyboardExitPrompt = "<voffset=-0.1em><sprite=\"R\" anim=\"0,2,6\"></voffset> to restart and <voffset=-0.1em><sprite=\"Q\" anim=\"0,2,6\"></voffset> to quit game";

        public static string ResultsScreenTitlePrompt = "Results";
        public static string ResultsScreenPointsPrompt = "Points";
    }
}
