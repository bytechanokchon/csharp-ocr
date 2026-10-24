using System;
using System.Collections.Generic;
using System.Text;

namespace OCR.dtos
{
    public class KeyValueDto
    {
        public required string Title { get; set; }
        public required StringBuilder Value { get; set; }
    }
}
