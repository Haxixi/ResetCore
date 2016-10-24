using UnityEngine;
using System.Collections;
using System;

namespace ResetCore.NetPost
{
    public abstract class BaseHandler
    {
        public abstract void Handle(Package package);
    }

    public class TestHandler : BaseHandler
    {
        public override void Handle(Package package)
        {
            throw new NotImplementedException();
        }
    }
}
