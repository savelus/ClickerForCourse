﻿using System.Collections.Generic;
using UnityEngine.Events;

namespace Global.SaveSystem.SavableObjects {
    public class OpenedSkills : ISavable {
        public List<SkillWithLevel> Skills = new() {
            new SkillWithLevel() {
                Id = "BlueDamageSkill",
                Level = 1
            },
        };

        public SkillWithLevel GetOrCreateSkillWithLevel(string skillId) {
            foreach (var skillWithLevel in Skills) {
                if (skillWithLevel.Id == skillId) {
                    return skillWithLevel;
                }
            }

            var newSkill = new SkillWithLevel() {
                Id = skillId,
                Level = 0
            };
            Skills.Add(newSkill);

            return newSkill;
        }
    }
}