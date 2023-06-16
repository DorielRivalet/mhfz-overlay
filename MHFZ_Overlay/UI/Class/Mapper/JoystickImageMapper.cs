﻿// © 2023 The mhfz-overlay developers.
// Use of this source code is governed by a MIT license that can be
// found in the LICENSE file.
using MHFZ_Overlay.Core.Enum;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace MHFZ_Overlay.UI.Class.Mapper;

public static class JoystickImageMapper
{
    public static IReadOnlyDictionary<Direction, string> imagePaths = new Dictionary<Direction, string>
    {
        { Direction.None, "UI/Icons/png/gamepad_joystick.png" },
        { Direction.Up, "UI/Icons/png/gamepad_joystick_up.png" },
        { Direction.UpRight, "UI/Icons/png/gamepad_joystick_upright.png" },
        { Direction.Right, "UI/Icons/png/gamepad_joystick_right.png" },
        { Direction.DownRight, "UI/Icons/png/gamepad_joystick_downright.png" },
        { Direction.Down, "UI/Icons/png/gamepad_joystick_down.png" },
        { Direction.DownLeft, "UI/Icons/png/gamepad_joystick_downleft.png" },
        { Direction.Left, "UI/Icons/png/gamepad_joystick_left.png" },
        { Direction.UpLeft, "UI/Icons/png/gamepad_joystick_upleft.png" }
    };

    public static string GetImage(Direction direction)
    {
        if (imagePaths.TryGetValue(direction, out var imagePath))
        {
            return imagePath;
        }

        // Return a default image path or handle the case where direction is not found
        return "UI/Icons/png/gamepad_joystick.png";
    }
}