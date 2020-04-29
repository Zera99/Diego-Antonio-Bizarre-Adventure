﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    IController SetModel(PlayerModel pl);
    void ListenKeys();
    void ListenFixedKeys();
}
