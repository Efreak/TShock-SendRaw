TShock-SendRaw
==============

Send raw commands and impersonate other users in tshock

What does it do?
==============
Sendraw allows you to broadcast raw messages to the server, without having them prefixed by (Server Broadcast). It also allows you to impersonate other users.

Permissions:
==============
sendraw - allows users to use /sendraw

sendred - allows users to use /sendred

sendwhite - allows users to use /sendwhite

sendas - allows users to use /sendas

Commands:
==============
/sendraw *message* - *message* is broadcast to the server as a normal message

/sendred *message* - *message* is broadcast in red text

/sendwhite *message* - *message* is broadcast in white text

/sendas *player name* **message** - **message** is broadcast to the server as though *player name* has said it, with the correct group color, prefix, and suffix

To Do:
==============
Perhaps hardcode it so sendas can only be used by superadmins? Not sure.

A command to replace /sendwhite and /sendred that allows you to choose from some basic colors

A command to further customize the color by allowing you to choose the exact rgb for it.

Allow you to use sendas using players who are not currently logged in (I think this may require looking at the database, not sure).

Send a note to the log every time this command is used.