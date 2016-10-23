using UnityEngine;
using System.Collections;
using ProtoBuf;

[ProtoContract]
public class TestProtobuf {

    [ProtoMember(1)]
    public int testData;

    [ProtoMember(2)]
    public string testString;

}
