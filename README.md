# VOX 2.0

An upgraded version of my old TTS Interface batchfile. It's included in this project for comparison's sake.

Included here is BasicRender 2.0, VBTyper, and the Speaker class, all of which are mine, and may be reused by anyone, as long as I can get a little credit. 

## StoryTime
Vox enters StoryTime mode when it's launched with a text file as an arguement. The text file must be formatted in a special way, but it is human readable.

Below is an example of a short story:

```
A Short Story
by Some bot

:2 Actors:

Narrator,F
Narrator 2,7
================================

[Narrator]
Once upon a time

[Narrator 2]
The end

[Narrator]
Really?

[Narrator 2]
yes
```

The format essentially boils down to:
```
(Title)
by (Author)

:(# of actors) Actors:

CHARACTER,(Color of this character's text as hexadecimal)
================================

[CHARACTER]
What to say
```

As of now, StoryTime also lets you change pitch, volume, and speed of characters, using the following syntax anywhere in the text:
```
{CHARACTER_NAME,VOL,##} where ## goes from 0 to 100
{CHARACTER_NAME,SPEED,##} Where ## goes from -10 to 10
{CHARACTER_NAME,PITCH,##} Where ## goes from -10 to 10
```

## OneCore voice support
It's recommended you download more voices from microsoft to have more available actors, however Vox2 is not compatible with OneCore voices. (I'm unsure as to how to actually make it compatible). However, there are workarounds available online. See this guide if you're intent on doing this (Do note that it does involve modifying the registry. I take no responsibility for what may happen if you do this.)

https://www.ghacks.net/2018/08/11/unlock-all-windows-10-tts-voices-system-wide-to-get-more-of-them/

