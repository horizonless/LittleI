using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class LevelTwoConfig : ScriptableObject
{
	public List<LevelTwoConfigEntity> LevelTwoConfigEntities; // Replace 'EntityType' to an actual type that is serializable.
}
