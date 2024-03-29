﻿using Core.Utilities.Results;
using Entities.Concrete.ForForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Business.Abstract
{
    public interface IFormSettingService
    {
        IDataResult<List<FormSetting>> GetAll();
        IResult Add(FormSetting formSetting);
        IResult Update(FormSetting formSetting);
        IResult Delete(FormSetting formSetting);
        IDataResult<FormSetting> GetById(int id);
        IDataResult<FormSetting> GetByName(string name);

        IDataResult<Size> GetUsbBarCodeScannerFormTrackBarValues();
        IResult UpdateUsbBarCodeScannerFormTrackBarValues(int width, int height);
    }
}
