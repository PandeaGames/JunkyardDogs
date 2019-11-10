﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class StageFormat : AbstractStaticData {
    [Range(1, 10), SerializeField]
    protected int _groups = 1;

    public abstract StageState GenerateState(int participants);
    public abstract StageState GenerateState(StageState lastStage);
}