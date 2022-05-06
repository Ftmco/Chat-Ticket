using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.DataBase.ViewModel;

public record UploadFileViewModel(Guid? FileId,string? FileToken);