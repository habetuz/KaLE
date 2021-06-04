// <copyright file="GameSense.Animation.IKeyAnimator.cs">
// Copyright (c) 2021. All Rights Reserved
// </copyright>
// <author>
// Marvin Fuchs
// </author>
// <summary>
// Visit https://marvin-fuchs.de for more information
// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSense.Animation
{
    public interface IKeyAnimator : IAnimator
    {
        bool Finished { get; }
        Key Key { get; set; }
    }
}
