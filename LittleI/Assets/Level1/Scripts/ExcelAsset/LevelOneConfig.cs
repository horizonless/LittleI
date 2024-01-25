using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class LevelOneConfig : ScriptableObject
{
	public List<LevelOneConfigEntity> LevelOneConfigEntities; // Replace 'EntityType' to an actual type that is serializable.
}
