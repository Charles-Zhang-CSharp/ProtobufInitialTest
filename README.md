# ProtoBuf Initial Test

Nowadays I find using C# standard libraries and implement my own binary serialization along with L4oz compression is fast and straightforward and gives more control - especially since BinaryFormatter is no longer available which I never liked anyway.

In this setup, we compare size, speed and overall cleaniess between binary formatter, custom binary serialization with compression, and protobuf.

|Criteria|Custom Binary Implementation|JSON/BSON|Binary Formatter|ProtoBuf|
|-|-|-|-|-|
|Availability|The only one issue with C# is at the movement we haven't figured out how exactly does it serialize string.|Libraries are available for all programming languages, and it's not too hard to implement a reader on one's own.|Completely unusable outside .Net ecosystem; No one knows what's inside.|Packages available for most programming languages; It's said that Protobuf can provide predictable file size and file structure description files?|
|Size|Very optimal.|Bulky, but human readable.|As bulky as plain-text JSON.|Decent - and it's said that the size for Protobuf is predictable?|
|Speed|Fastest|Slowest|No faster than anything|Fast, only second to custom implementation|
|Programming Easiness|Requires a tiny bit design effort - can be daunting to beginners.|No programming needed|No programming needed|No programming needed|
|Fun|Much more fun because of custom design.|Human readable|-|-|
|Code Cleaniness and Maintainability|Perfect decoupling - data class is just data class.|Both Newtonsoft.JSON and .net system library JSON has a bunch of ugly attributes.|Ugly, disguting, confusing, distracting attributes.|Very very ugly and intrusive attributes.|
|Capability|Can do anything.|Some say it has restrictions in terms of type information.|Most capable - this thing can literally dump memory, with pointers and all other structures.|***PENDING INVESTIGATION***|
|Compatibility|Needs custom implementation but if the file format is well designed it shouldn't be too difficult.|Completely incompatible with non-.Net Framework applications.|Supposedly compatible out of box with Python.|Might require some custom code but generally easily done; Directly usable in JavaScript.|
|Portability|Again, except the string concern, it's highly portable - when without compression.|Highly compatible - plug-in-and-play in JavaScript|Very hard to impossible to convert - reliance on this makes code and data structures extremely messy and bad.|Decently portable and cross-platform.|

## Results

Results per debug build in Visual Studio: (*10000* items with trivial `string` + `double` data)

| Operation       | Time taken | File size  |
|-----------------|------------|------------|
| Write to Binary | 0:00.396   | 3.00Mb     |
| Write to JSON   | 0:00.565   | 3.00Mb     |
| Write to Protobuf| 0:00.177   | 2.00Mb     |
| Write to Custom | 0:00.146   | 685.00 Kb  |


## Verdicts

* ProtoBuf
* Custom Binary Implementation
* Binary formatter: We will never use it ever again, it's shown here just for comparison purpose and comprehensiveness.