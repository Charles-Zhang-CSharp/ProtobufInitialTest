# ProtoBuf Initial Test

Nowadays I find using C# standard libraries and implement my own binary serialization along with L4oz compression is fast and straightforward and gives more control - especially since BinaryFormatter is no longer available which I never liked anyway.

In this setup, we compare size, speed and overall cleaniess between binary formatter, custom binary serialization with compression, and protobuf.

|Criteria|Custom Binary Implementation|JSON/BSON|Binary Formatter|ProtoBuf|
|-|-|-|-|-|
|Availability|The only one issue with C# is at the movement we haven't figured out how exactly does it serialize string.|-|-|-|
|Size|||
|Speed|||
|Programming Easiness|Requires a tiny bit design effort - can be daunting to beginners.||
|Fun|Much more fun because of custom design.||
|Code Cleaniness and Maintainability|Both Newtonsoft.JSON and .net system library JSON has a bunch of ugly attributes.||Ugly, disguting, confusing, distracting attributes.|Very very ugly and intrusive attributes.|
|Capability|Can do anything.||
|Compatibility|Needs custom implementation but if the file format is well designed it shouldn't be too difficult.|Completely incompatible with non-.Net Framework applications.|Supposedly compatible out of box with Python.|Might require some custom code but generally easily done; Directly usable in JavaScript.|
|Portability|||Very hard to impossible to convert - reliance on this makes code and data structures extremely messy and bad.|-|

## Results

Results per debug build in Visual Studio:

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