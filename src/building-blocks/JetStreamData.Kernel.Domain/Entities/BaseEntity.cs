// Copyright (c) Admir Mujkic for Journey Mentor Cyprus LTD.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

namespace JetStreamData.Kernel.Domain.Entities;

public abstract class BaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
}
