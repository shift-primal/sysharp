# sysharp

A small command-line system report for Linux, written in C#. It reads your hardware through standard Linux tools and prints a tidy overview of CPU, memory sticks, drives and GPU.

## What it shows

| Part   | Source                         |
| ------ | ------------------------------ |
| CPU    | `lscpu`                        |
| Memory | `dmidecode --type 17` (needs sudo) |
| Drives | `lsblk`                        |
| GPU    | `lspci -v`                     |

## Run it

Requires the .NET 10 SDK and a Linux machine with the tools above installed.

```sh
git clone https://github.com/shift-primal/sysharp
cd sysharp
dotnet run --project src/sysharp
```

You'll be asked for your sudo password so `dmidecode` can read the memory sticks.

## How it's built

```
src/sysharp/
  Collectors/   runs the shell commands and parses their output
  Models/       Computer, Cpu, MemoryStick, Drive, Gpu
  Services/     CommandRunner (process wrapper), Formatting
  UI/           ConsoleRenderer
```

`Computer.Collect()` gathers each part through the collectors, and the renderer walks the model and prints one section per part.
