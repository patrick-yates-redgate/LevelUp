###AsyncDumpToGraph

A very early stage project to read the output from the `windbg` command `!dumpasync` and aggregate it into something a little more useful.

Longer term plan would be to output as an html file or something that would allow you to expand/collapse in order to drill into the data with more control.

##Example
Takes output that looks like this:
![image](https://github.com/user-attachments/assets/a80fa671-55f1-444e-8696-8ad5efe6c34e)

And it finds common stack paths and produces this (where the `Objects` number is the number of stacks that have the exact same call stack to that point):
![image](https://github.com/user-attachments/assets/afe975e4-a639-405c-8d7c-97dcf0b01815)
