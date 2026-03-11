<template>
  <div class="content">
    <!-- thêm mới bản ghi -->
    <div
      class="content-header"
      v-if="idConfirm == 0 || mode === 'cloningSalary'"
    >
      <div style="display: flex; align-items: center">
        <div class="next-page" @click="modalConfirm">
          <div class="icon-system-catalog"></div>
        </div>
        <div class="text-ingredient">Thêm thành phần</div>
      </div>
      <div class="header-right">
        <div class="header-items">
          <div class="cancel" @click="goBack">
            <div>Hủy bỏ</div>
          </div>
          <div class="cancel-add" @click="insertNotGoBack">
            <div class="text">Lưu và thêm</div>
          </div>
          <div class="save" @click="insertGoBack">
            <div>Lưu</div>
          </div>
        </div>
      </div>
    </div>
    <!-- cập nhật bản ghi  -->
    <div class="update_hearder" v-else>
      <div style="display: flex; align-items: center">
        <div class="next-page" @click="modalConfirm">
          <div class="icon-system-catalog"></div>
        </div>
        <div class="text-label">{{ compositionName }}</div>
      </div>
      <div style="display: flex; align-items: center">
        <div class="header-items">
          <div class="cancel-update" @click="goBack">
            <div>Hủy bỏ</div>
          </div>
          <div class="update" @click="saveValueUpdate">
            <div>Lưu</div>
          </div>
          <div class="icon-box">
            <div class="icon_update"></div>
          </div>
        </div>
      </div>
    </div>
    <!--  content -->
    <div class="add-salary-content">
      <!--  tên thành phần -->
      <div class="form-row">
        <div class="text-icon">
          <div class="flex a-i" style="width: 186px">
            <div class="text">Tên thành phần</div>
            <div class="redStar">*</div>
          </div>
        </div>
        <MsInput
          ref="onFocus"
          v-model="model.SalaryCompositionName"
          :required="true"
          :error="isValidateName"
        />
      </div>
      <!--Mã thành phần -->
      <div class="form-row">
        <div class="text-icon">
          <div class="flex a-i" style="width: 186px">
            <div class="text">Mã thành phần</div>
            <div class="redStar">*</div>
          </div>
        </div>

        <MsInput
          :class="{ disabledCode: mode === `editSalary` }"
          v-model="model.SalaryCompositionCode"
          placeholder="Nhập mã viết liền "
          :required="true"
          :error="isValidateCode"
        />
      </div>
      <!-- đơn vị áp dụng -->
      <div class="form-row">
        <div class="text-icon">
          <div class="flex a-i" style="width: 186px">
            <div class="text">Đơn vị áp dụng</div>
          </div>
        </div>
        <MsTagBox
          class="autoSum"
          :items="unitTreeData"
          v-model="model.OrganizationUnitData"
          @changeData="changeOrganizationUnit"
        />
      </div>
      <!-- loại thành phần -->
      <div class="form-row">
        <div class="text-icon">
          <div class="flex a-i" style="width: 186px">
            <div class="text">Loại thành phần</div>
            <div class="redStar">*</div>
          </div>
        </div>
        <MsSelectBox
          class="component_type"
          :items="componentTypeOptions"
          v-model="model.ComponentType"
          :required="true"
          :error="isValidateValue"
        />
      </div>
      <!-- tính chất -->
      <div class="form-row">
        <div class="text-icon">
          <div class="flex a-i" style="width: 186px">
            <div class="text">Tính chất</div>
            <div class="redStar">*</div>
          </div>
        </div>
        <div class="flex a-i">
          <MsSelectBox
            class="properties"
            :items="optionProperties"
            v-model="model.Nature"
            :required="true"
            :error="isValidateProperties"
          />
          <div class="flex a-i" style="margin-left: 16px">
            <!--khi chọn thu nhập-->
            <MsRadioBox
              :items="ListNatureOption"
              v-model="model.TaxableType"
              v-if="model.Nature === MsEnum.Nature.Income"
            />
            <!-- khi chọn khấu trừ -->
            <MsCheckBox
              label="Giảm trừ khi tính thuế"
              v-model="model.IsDeductedOnTax"
              v-if="model.Nature === MsEnum.Nature.Deduction"
            />
          </div>
        </div>
      </div>
      <!-- ( khi chọn khấu trừ or thu nhập trong tính chất )-->
      <div
        v-if="
          model.Nature === MsEnum.Nature.Income ||
          model.Nature === MsEnum.Nature.Deduction
        "
      >
        <!--  định mức -->
        <div class="form-row">
          <div class="text-icon">
            <div class="flex a-i" style="width: 186px">
              <div class="text">Định mức</div>
            </div>
          </div>
          <MsFormulaTextarea
            v-model="model.Quota"
            placeholder="Tự động gợi ý công thức và tham số khi gõ"
          />
        </div>
        <!-- cho phép quá định mức -->
        <div class="form-row">
          <div class="text-icon"></div>
          <MsCheckBox
            class="exceedingLimit"
            label="Cho phép tính giá trị vượt quá định mức"
            v-model="model.IsExceedQuota"
          />
          <div class="iconExceedingLimit">
            <div class="isIcon"></div>
          </div>
        </div>
      </div>
      <!-- Kiểu giá trị -->
      <div class="form-row">
        <div class="text-icon">
          <div class="flex a-i" style="width: 186px">
            <div class="text">Kiểu giá trị</div>
          </div>
        </div>
        <MsSelectBox
          class="component_type"
          :items="valueTypeOption"
          :class="{
            disabled:
              model.Nature === MsEnum.Nature.Income ||
              model.Nature === MsEnum.Nature.Deduction,
          }"
          v-model="model.SalaryValueType"
        />
      </div>
      <!--  giá trị -->
      <div class="form-row">
        <div class="text-icon">
          <div class="flex a-i" style="width: 186px">
            <div class="text">Giá trị</div>
          </div>
        </div>
        <div class="radio-column">
          <!-- Tự động cộng tổng -->
          <div
            class="radio-row"
            v-if="
              model.SalaryValueType === MsEnum.ValueTypeEnum.Number ||
              model.SalaryValueType === MsEnum.ValueTypeEnum.Currency
            "
          >
            <MsRadioBox
              v-model="model.SalaryValue"
              :items="[
                {
                  label: `Tự động cộng tổng giá trị của các nhân viên`,
                  value: MsEnum.CalculationMethodEnum.AutoSum,
                },
              ]"
            />
            <!-- trong cùng đơn vị công tác -->
            <MsSelectBox
              class="workUnit"
              :items="listAggregationScope"
              v-model="model.ValueArrgegatitonScope"
              :class="{
                disabled:
                  model.SalaryValue ===
                  MsEnum.CalculationMethodEnum.CustomFormula,
              }"
            />
          </div>
          <!-- chọn thành phần lương để cộng giá trị -->
          <MsSelectBox
            class="autoSum"
            :items="sumValueOptions"
            v-model="model.SumValue"
            v-if="model.SalaryValue === MsEnum.CalculationMethodEnum.AutoSum"
          />
          <!-- tính theo công thức tự đặt -->
          <div
            class="radio-row"
            v-if="
              model.SalaryValueType === MsEnum.ValueTypeEnum.Number ||
              model.SalaryValueType === MsEnum.ValueTypeEnum.Currency
            "
          >
            <MsRadioBox
              v-model="model.SalaryValue"
              :items="[
                {
                  label: 'Tính theo công thức tự đặt',
                  value: MsEnum.CalculationMethodEnum.CustomFormula,
                },
              ]"
            />
          </div>
          <!-- ô input -->
          <MsFormulaTextarea
            placeholder="Tự động gợi ý công thức và tham số khi gõ"
            v-model="model.RecipeSuggestion"
            v-if="
              model.SalaryValue === MsEnum.CalculationMethodEnum.CustomFormula
            "
          />
        </div>
      </div>
      <!--  mô tả -->
      <div class="form-row">
        <div class="text-icon">
          <div class="flex a-i" style="width: 186px">
            <div class="text">Mô tả</div>
          </div>
        </div>
        <MsTextarea v-model="model.SalaryDescription" />
      </div>
      <!-- Hiển thị trên phiếu lương -->
      <div class="form-row">
        <div class="text-icon">
          <div class="flex a-i" style="width: 186px">
            <div class="text">Hiển thị trên phiếu lương</div>
          </div>
        </div>
        <MsRadioBox
          :items="listShowSalaryOptions"
          v-model="model.ShowOnPayslip"
        />
      </div>
      <!-- Nguồn tạo -->
      <div class="form-row">
        <div class="text-icon">
          <div class="flex a-i" style="width: 186px">
            <div class="text">Nguồn tạo</div>
          </div>
        </div>
        <div class="extra">Tự thêm</div>
      </div>
    </div>
  </div>
  <MsModalAdd
    v-if="showModal"
    @closeModal="closeConfirm"
    @saveValue="saveConfirm"
  />
</template>

<script setup>
import { onMounted, ref, reactive, watch, nextTick } from "vue";
import { useRouter, useRoute } from "vue-router";
import MsEnum from "@/enums/enums.js";
import SalaryCompositionService from "@/services/salaryCompositionAPI";
import OrganizationService from "@/services/organizationAPI";
import SalaryComposition from "@/model/salaryComposition.js";
// toast
import { activateToast } from "@/utils/toast";
// import các options
import {
  componentTypeOptions,
  optionProperties,
  ListNatureOption,
  valueTypeOption,
  listAggregationScope,
  sumValueOptions,
  listShowSalaryOptions,
} from "@/components/controls/Options.js";
import MsInput from "../../components/controls/MsInput.vue";
import MsTextarea from "../../components/controls/MsTextarea.vue";
const onFocus = ref(null);
///
///  điều hướng router
///
const router = useRouter();
const goBack = () => {
  router.push("/salarycomposition");
};

///
/// đơn vị áp dụng (tree)
///

// lấy dữ liệu bảng đơn vị cơ cấu tổ chức
const unitTreeData = ref([]);
const loadOrganizationData = async () => {
  try {
    const res = await OrganizationService.getAll();
    if (res && res.data) {
      const organizationUnitValue = res.data.map((item) => ({
        id: item.organizationUnitId,
        parentId:
          item.parentId && item.parentId !== null ? item.parentId : null,
        label: item.organizationUnitName,
      }));
      unitTreeData.value = organizationUnitValue;

      if (unitTreeData.value.length > 0 && !route.params.id) {
        // lưu mảng id cập nhật v-model
        model.OrganizationUnitData = unitTreeData.value.map((item) => item.id);
      }
    }
  } catch (error) {
    consolelog("Lỗi lấy dữ liệu:", error);
  }
};
// nhận dữ liệu từ tree ( object)
const OrganizationUnitObject = ref([]);
const changeOrganizationUnit = (data) => {
  OrganizationUnitObject.value = data;
};
///`
/// khởi tạo khi thêm mới
///
const model = reactive(new SalaryComposition());
const initData = () => {
  model.SalaryCompositionName = "";
  model.SalaryCompositionCode = "";
  model.Quota = "";
  // giá trị
  model.RecipeSuggestion = ""; // tự động gợi ý công thức và tham số khi gõ
  model.SalaryValue = MsEnum.CalculationMethodEnum.CustomFormula; // giá trị : check box
  model.SumValue = MsEnum.SumValueOptionKey.addSalaryComponentValue; // chọn thành phần lương để cộng giá trị
  model.ValueArrgegatitonScope = MsEnum.AggregationScopeEnum.SameDepartment; // trong cùng đơn vị công tác
  //
  model.SalaryDescription = "";
  // tính chất
  model.Nature = MsEnum.Nature.Income;
  model.TaxableType = MsEnum.NatureOption.Taxable;
  model.IsDeductedOnTax = false;
  //
  model.SalaryValueType = MsEnum.ValueTypeEnum.Currency;
  model.ShowOnPayslip = MsEnum.SalaryShowOption.Yes;
  model.OrganizationUnitData = [];
  model.ComponentType = "";
};

///
/// hàm dùng chung thêm mới và cập nhật
///

const dataToSubmit = () => {
  // kiểm tra Giá Trị
  let findSumValue = "";
  let findRecipeSuggestion = "";
  if (model.SalaryValue === MsEnum.CalculationMethodEnum.CustomFormula) {
    // Nếu chọn Công thức tự đặt -> Lấy giá trị từ RecipeSuggestion
    findRecipeSuggestion = String(model.RecipeSuggestion || "");
    findSumValue = "";
  } else {
    // Nếu chọn Tự động cộng tổng -> Lấy giá trị từ SumValue
    findSumValue = String(model.SumValue || "");
    findRecipeSuggestion = "";
  }
  return {
    // lấy tất cả thuộc tính trong model
    ...model,
    SalaryCompositionId: String(idConfirm.value || 0),
    // đơn vị áp dụng
    // chuyển thành string json
    OrganizationUnitData: JSON.stringify(OrganizationUnitObject.value || []),
    // giá trị
    SumValue: findSumValue,
    RecipeSuggestion: findRecipeSuggestion,
    SalaryValue: String(model.SalaryValue || ""),
    ValueArrgegatitonScope: String(model.ValueArrgegatitonScope || ""),
    Quota: model.Quota != null ? String(model.Quota) : "",
    ShowOnPayslip: Number(model.ShowOnPayslip) || 0,
    Source: "Tự thêm",
    Status: 1,
    // CreatedAt: new Date().toISOString(),
    // UpdatedAt: new Date().toISOString(),
  };
};

///
/// thêm mới
///
const insertSalary = async () => {
  // validate
  if (validate()) {
    try {
      // lấy từ khóa nhập
      const keyWord = model.SalaryCompositionCode;
      const ress = await SalaryCompositionService.GetByKeyWord(keyWord);
      if (ress.data?.length > 0) {
        activateToast("error", "Mã thành phần đã tồn tại");
        return false;
      } else {
        var data = dataToSubmit();

        const res = await SalaryCompositionService.insert(data);
        if (res && res.data.success) {
          activateToast("success", "Thêm thành công");
          return true;
        } else {
          const msg = res.data.userMsg;
          activateToast("error", msg);
        }
      }
    } catch (error) {
      console.log("lỗi thêm mới:", error);
      activateToast("error", "Thêm thành phần lương thất bại");
    }
  }
};
const insertGoBack = async () => {
  const res = await insertSalary();
  if (res) {
    goBack();
  }
};
const insertNotGoBack = async () => {
  const res = await insertSalary();
  if (res) {
    loadOrganizationData();
    initData();
  }
};
///
/// sửa bản ghi
///
const saveValueUpdate = async () => {
  if (validate()) {
    try {
      const res = await SalaryCompositionService.update(dataToSubmit());
      if (res && res.data.success) {
        initData();
        activateToast("success", "Cập nhật thành phần lương thành công");
        goBack();
      } else {
        const msg = res.data.userMsg;
        activateToast("error", msg);
      }
    } catch (error) {
      console.log("Cập nhật thất bại", error);
      activateToast("error", "Cập nhật thành phần lương thất bại");
    }
  }
};

///
/// khởi tạo khi sửa và nhân bản
///
const compositionName = ref("");
const route = useRoute();
const idConfirm = ref();
const mode = route.query.mode;
//const mode = ref();
onMounted(async () => {
  await loadOrganizationData();
  initData();
  // lấy id  qua router
  const id = route.params.id;
  // // lất query qua router
  // mode.value = route.query.mode;
  //console.log("string để kiểm tra là:", mode.value);
  idConfirm.value = id;
  if (id) {
    try {
      // lấy data bảng thành phần lương
      const res = await SalaryCompositionService.getById(id);
      // lấy tên thành phần
      compositionName.value = res.data.salaryCompositionName;
      // map dữ liệu
      const dataDetail = res?.data;
      if (mode === "editSalary") {
        model.SalaryCompositionName = dataDetail.salaryCompositionName;
        model.SalaryCompositionCode = dataDetail.salaryCompositionCode;
      }
      if (mode === "cloningSalary") {
        model.SalaryCompositionName = null;
        model.SalaryCompositionCode = null;
      }
      model.ComponentType = dataDetail.componentType;
      model.Nature = dataDetail.nature;
      model.TaxableType = dataDetail.taxableType;
      model.IsDeductedOnTax = dataDetail.isDeductedOnTax;
      model.Quota = dataDetail.quota;
      model.IsExceedQuota = dataDetail.isExceedQuota;
      // kiểu giá trị
      model.SalaryValueType = dataDetail.salaryValueType;
      model.SalaryDescription = dataDetail.salaryDescription;
      //--------- đơn vị áp dụng
      if (dataDetail.organizationUnitData) {
        const confirmOrg = JSON.parse(dataDetail.organizationUnitData || "[]");
        // lấy các trường id
        const idsOrg = confirmOrg.map((item) => item.organizationUnitId);
        model.OrganizationUnitData = idsOrg;
        OrganizationUnitObject.value = confirmOrg;
      }
      //-------- giá trị : BE string -> ép thành number
      if (dataDetail.recipeSuggestion) {
        model.RecipeSuggestion = dataDetail.recipeSuggestion;
        model.SalaryValue = Number(dataDetail.salaryValue || 0);
        model.SumValue = MsEnum.SumValueOptionKey.addSalaryComponentValue;
      }
      if (dataDetail.sumValue) {
        model.SumValue = Number(dataDetail.sumValue);
        model.SalaryValue = Number(dataDetail.salaryValue);
      }
      model.ValueArrgegatitonScope = dataDetail.valueArrgegatitonScope
        ? Number(dataDetail.valueArrgegatitonScope)
        : null;
      model.ShowOnPayslip = Number(dataDetail.showOnPayslip);
    } catch (error) {
      console.log("Lỗi lấy dữ liệu bản ghi:", error);
    }
  }

  ///
  /// focus khi mở form thêm mới
  ///
  await nextTick();
  onFocus.value?.focus();
});

///
/// theo dõi nhập giá trị tên thành phần để nhập giá trị
///
watch(
  () => model.SalaryCompositionName,
  (val) => {
    // Nếu có idConfirm form sửa
    if (idConfirm.value && mode === "editSalary") {
      return;
    }
    // Nếu không có idConfirm - form them mới
    if (val) {
      const valueToUpperCase = val
        .normalize("NFD")
        .replace(/[\u0300-\u036f]/g, "")
        .replace(/đ/g, "d")
        .replace(/Đ/g, "D")
        .replace(/\s+/g, "_")
        .toUpperCase();
      model.SalaryCompositionCode = valueToUpperCase;
    } else {
      model.SalaryCompositionCode = "";
    }
  },
);
///
/// Mở MsModalAdd
///
const showModal = ref(false);
// mở modal
const modalConfirm = () => {
  const hasChanged =
    model.SalaryCompositionName?.trim() ||
    model.SalaryCompositionCode?.trim() ||
    model.ComponentType;
  if (hasChanged) {
    showModal.value = true;
  } else {
    goBack();
  }
};
// đóng modal
const closeConfirm = () => {
  showModal.value = false;
};

// nút lưu thêm mới or cập nhật
const saveConfirm = async () => {
  const confirmValidate = validate();
  if (!confirmValidate) {
    showModal.value = false;
    return;
  }
  if (idConfirm.value) {
    await saveValueUpdate();
  } else {
    await insertSalary();
  }
  showModal.value = false;
  goBack();
};

///
/// validate
///
const isValidateName = ref("");
const isValidateCode = ref("");
const isValidateValue = ref("");
const isValidateProperties = ref("");
const validate = () => {
  let isValid = true;
  // tên thành phần không được để trống
  if (!model.SalaryCompositionName) {
    isValidateName.value = "Không được để trống.";
    isValid = false;
  } else if (model.SalaryCompositionName.length > 255) {
    isValidateName.value = "Không vượt quá 255 ký tự.";
    isValid = false;
  }
  // mã thành phần không được để trống
  if (!model.SalaryCompositionCode) {
    isValidateCode.value = "Không được để trống.";
    isValid = false;
  } else if (model.SalaryCompositionCode.length > 255) {
    isValidateCode.value = "Không vượt quá 255 ký tự.";
    isValid = false;
  }
  // loại thành phần không được để trống
  if (!model.ComponentType) {
    isValidateValue.value = "Không được để trống.";
    isValid = false;
  }
  // tính chất không được để trống
  if (!model.Nature) {
    isValidateProperties.value = "Không được để trống.";
    isValid = false;
  }
  return isValid;
};
// theo dõi giá trị truyền vào và cập nhật luôn
watch(
  () => model.SalaryCompositionName,
  (newVal) => {
    if (newVal && newVal.trim() !== "") {
      isValidateName.value = "";
    }
  },
);
watch(
  () => model.SalaryCompositionCode,
  (newVal) => {
    if (newVal && newVal.trim() !== "") {
      isValidateCode.value = "";
    }
  },
);

watch(
  () => model.ComponentType,
  (newVal) => {
    if (newVal) {
      isValidateValue.value = "";
    }
  },
);
watch(
  () => model.Nature,
  (newVal) => {
    if (newVal) {
      isValidateProperties.value = "";
    }
  },
);
//   console.log("msValue hiện tại:", msValue.value);
// console.log("Các giá trị trong items:", msValueOption.map(i => i.value));
</script>
<style scoped lang="scss">
.flex {
  display: flex;
}

.a-i {
  align-items: center;
}

.j-i {
  justify-items: center;
}

.f-d {
  flex-direction: column;
}
.d-bl {
  display: block;
}
.content {
  padding: 16px 24px;
  height: calc(100vh - 48px);
  background-color: #f2f2f2;
  flex: 1;
  overflow: auto;

  //title- header
  .content-header {
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding-bottom: 16px !important;

    .next-page {
      margin-right: 8px;
      cursor: pointer;
      display: flex !important;
      align-items: center !important;
      justify-content: center !important;
      width: 36px !important;
      height: 36px !important;
      border-radius: 50% !important;
      border: none !important;
      outline: none !important;
    }

    .next-page:hover {
      background-color: #dadce3;
    }

    .text-ingredient {
      font-weight: 700;
      font-size: 20px;
      letter-spacing: 0.384px !important;
      margin-right: 8px !important;
    }
    .header-right {
      display: flex;
      align-items: center;
      font-size: 14px;
      font-weight: 500;
    }

    .header-items {
      display: flex;
      align-items: center;

      .cancel {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 80px;
        height: 36px;
        border-radius: 4px;
        background-color: #fff;
        color: #212121;
        border: 1px solid #ebebeb;
        margin-right: 8px;
        font-size: 14px;
        cursor: pointer;
      }
      .cancel:hover {
        background-color: #fff;
        color: #02b936;
        border: 1px solid #02b936;
      }

      .cancel-add {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 106px !important;
        height: 36px !important;
        border-radius: 4px;
        padding: 0px 16px;
        min-width: 0px !important;
        background-color: #fff;
        color: #212121;
        border: 1px solid #ebebeb;
        margin-right: 8px;
        font-size: 14px;
        cursor: pointer;
      }
      .cancel-add:hover {
        background-color: #fff;
        color: #02b936;
        border: 1px solid #02b936;
      }

      .save {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 80px !important;
        height: 36px !important;
        border-radius: 4px;
        padding: 0px 16px;
        min-width: 0px !important;
        background-color: #34b057;
        color: #fff;
        border: 1px solid #34b057;
        cursor: pointer;
      }

      .text {
        display: flex;
        justify-content: center;
        align-items: center;
        white-space: nowrap;
      }
    }
  }

  // body-content
  .add-salary-content {
    padding: 40px;
    background: #fff;
    display: flex;
    flex-direction: column;
    flex: 1;
    overflow-y: auto;
    overflow-x: hidden;
    gap: 16px;

    .form-row {
      display: flex;
      width: 100%;
      text-indent: 0px;
      padding: 0px;
      margin-bottom: 16px;
      flex-flow: wrap;
      position: relative;

      .text-icon {
        display: flex;
        padding-right: 8px !important;
        width: 186px;
        min-height: 35px;
        font-size: 14px;
        font-weight: 700;
      }

      .redStar {
        color: red;
        padding-left: 2px;
      }
    }
    // cho phép giá trị định mức
    .iconExceedingLimit {
      display: flex;
      justify-content: center;
      align-items: center;
      height: 36px;
      width: 36px;
      cursor: pointer;
    }
    // giá trị -----------
    .radio-column {
      display: flex;
      flex-direction: column; // xếp radio theo cột dọc
      gap: 8px; // khoảng cách giữa các dòng
    }

    .radio-row {
      display: flex;
      align-items: center;
      gap: 12px;
    }
    // tự thêm
    .extra {
      height: 36px;
      line-height: 35px;
      border-bottom: 1px solid #e0e0e0;
      width: 676px;
    }
  }
  // cập nhập bản ghi hearder
  .update_hearder {
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding-bottom: 16px !important;

    .next-page {
      margin-right: 8px;
      cursor: pointer;
      display: flex !important;
      align-items: center !important;
      justify-content: center !important;
      width: 36px !important;
      height: 36px !important;
      border-radius: 50% !important;
      border: none !important;
      outline: none !important;
    }

    .next-page:hover {
      background-color: #cccccc;
    }

    .text-label {
      font-weight: 700;
      font-size: 20px;
      letter-spacing: 0.384px !important;
      margin-right: 8px !important;
    }

    .header-items {
      display: flex;
      align-items: center;

      .cancel-update {
        display: flex;
        align-items: center;
        width: 82px !important;
        height: 36px !important;
        border-radius: 4px;
        padding: 0px 16px;
        background-color: #fff;
        color: #02b936;
        border: 1px solid #02b936;
        margin-right: 8px;
        font-size: 14px;
        cursor: pointer;
      }
      .update {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 80px !important;
        height: 36px !important;
        border-radius: 4px;
        padding: 0px 16px;
        min-width: 0px !important;
        background-color: #34b057;
        color: #fff;
        border: 1px solid #34b057;
        cursor: pointer;
        margin-right: 8px;
      }

      .text {
        display: flex;
        justify-content: center;
        align-items: center;
        white-space: nowrap;
      }
      .icon-box {
        display: flex;
        align-items: center;
        justify-content: center;
        height: 36px;
        width: 36px;
        background-color: #fff;
        cursor: pointer;
        border: 1px solid #ebebeb;
        border-radius: 4px;
      }
      .icon-box:hover {
        border: 1px solid #34b057;
      }
    }
  }
}

///
/// style chèn trong controls (MsSelectBox- loại thành phần)
///
///
:deep(.component_type) {
  width: 237px;
}

:deep(.component_type .selectItems) {
  overflow-x: auto;
  overflow-y: auto;
  position: absolute;
  top: 100%;
  height: auto;
  bottom: auto;
  margin-top: 6px;
  z-index: 999;
}

///
/// style chèn trong controls (MsSelectBox- trong cùng đơn vị công tác)
///
///
:deep(.workUnit) {
  width: 237px;
}

:deep(.workUnit .selectBox) {
  width: 237px;
}

:deep(.workUnit .selectItems) {
  overflow-x: auto;
  overflow-y: auto;
  position: absolute;
  width: 237px;
  top: 100%;
  height: auto;
  bottom: auto;
  margin-top: 6px;
  z-index: 999;
}
///
/// style chèn trong controls (MsSelectBox- tính chất)
///

:deep(.properties .selectBox) {
  width: 237px;
}

:deep(.properties .selectItems) {
  width: 237px;
  position: absolute;
  top: 100%;
  bottom: auto;
  margin-top: 6px;
  z-index: 999;
}
///
/// style chèn trong controls (MsSelectBox- định mức)
///
:deep(.exceedingLimit.tax-option) {
  margin-left: 0px !important;
}
///
/// style chèn trong controls (MsSelectBox- giá trị)
///
:deep(.autoSum) {
  width: 676px;
}

:deep(.autoSum .selectItems) {
  overflow-x: auto;
  overflow-y: auto;
  position: absolute;
  width: 676px;
  height: auto;
  top: 100%;
  bottom: auto;
  margin-top: 6px;
  z-index: 999;
}

/// khóa sự kiện
///
:deep(.disabled) {
  cursor: not-allowed !important;
  pointer-events: none !important; /* Chặn click ngay từ lớp vỏ */
  opacity: 0.8;
}
:deep(.disabled .selectBox) {
  width: 237px;
  background-color: #e9eaee !important;
  cursor: not-allowed !important; /* Đổi con trỏ chuột thành biển cấm */
}
//
:deep(.disabledCode) {
  cursor: not-allowed !important;
  pointer-events: none !important; /* Chặn click ngay từ lớp vỏ */
  opacity: 0.8;
}
:deep(.disabledCode .input-text),
:deep(.disabledCode .input-text input) {
  background-color: #e9eaee !important;
  cursor: not-allowed !important;
}
</style>
