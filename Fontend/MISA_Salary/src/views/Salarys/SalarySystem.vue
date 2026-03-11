<template>
  <div class="content">
    <!-- header-box -->
    <div class="content-header">
      <div class="text-content-header">
        <div class="next-addSalary" @click="goBack">
          <div class="icon-system-catalog"></div>
        </div>
        <div>{{ $t("contentSystem.systemSalaryCategory") }}</div>
      </div>
    </div>
    <!-- main-content(body) -->
    <div class="content-main">
      <!-- content(header in box) -->
      <div class="grid-filter">
        <!-- phần tìm kiếm -->
        <div class="search-header" v-if="countSelectRows === 0">
          <div class="input-search-options flex">
            <div class="icon-search-options-box" style="padding-left: 8px">
              <div class="icon-scale-search"></div>
            </div>
            <input
              type="text"
              v-model="inputKeyWord"
              @keyup.enter="enter"
              :placeholder="$t('filter.search')"
            />
          </div>
        </div>
        <!-- click checkbox show ra -->
        <div class="grid-options" v-else>
          <div>
            {{ $t("filter.selected") }}
            <span class="textNumber">{{ countSelectRows }} </span>
          </div>
          <div class="text-label">{{ $t("filter.deselected") }}</div>
          <div class="addToUseLis" @click="addSalaryComposition">
            <div class="iconAddToUseLis">
              <div class="icon-addToUseLis"></div>
            </div>
            <div class="text-StopFolow">
              {{ $t("contentSystem.addToUseList") }}
            </div>
          </div>
        </div>
        <!-- phần bên phải tìm kiếm trong header -->
        <div class="options">
          <!-- tất cả thành phần -->
          <div class="statuses">
            <div>{{ $t("contentSystem.allSalaryComponents") }}</div>
            <div
              class="icon-dropdown"
              style="margin-left: 8px; cursor: pointer"
            ></div>
          </div>
          <!-- icon bộ lọc -->
          <div class="filter" @mouseenter="onTooltips($event, 'Bộ lọc')">
            <div class="icon-filter"></div>
          </div>
          <!-- icon setting -->
          <div class="setting" @mouseenter="onTooltips($event, 'Thiết lập')">
            <div class="icon-setting"></div>
          </div>
        </div>
      </div>
      <!--  xử dụng thự viện dxDatagrid -->
      <div class="grid-content">
        <div class="salary">
          <DxDataGrid
            ref="gridRef"
            :height="'100%'"
            :data-source="items"
            :column-auto-width="true"
            :row-alternation-enabled="false"
            :show-borders="false"
            :hover-state-enabled="true"
            :show-column-lines="false"
            :show-row-lines="true"
            :paging="{ enabled: false }"
            :pager="{ visible: false, showPageSizeSelector: false }"
            @selection-changed="onSelectionChanged"
          >
            <DxSelection
              mode="multiple"
              show-check-boxes-mode="always"
              :allow-select-all="true"
            />
            <!-- mã thành phần -->
            <DxColumn
              dataField="salaryCompositionSystemCode"
              caption="Mã thành phần"
              :minWidth="200"
              :maxWidth="360"
            />
            <!--  tên thành phần -->
            <DxColumn
              dataField="salaryCompositionSystemName"
              caption="Tên thành phần"
              :minWidth="200"
              :maxWidth="360"
            />

            <!--  loại thành phần -->
            <DxColumn
              dataField="componentType"
              caption="Loại thành phần"
              :minWidth="200"
              :maxWidth="360"
              alignment="left"
              :customize-text="formatComponentType"
            />
            <!--  tính chất -->
            <DxColumn
              dataField="nature"
              caption="Tính chất"
              :minWidth="200"
              :maxWidth="360"
              alignment="left"
              :customize-text="formatOptionProperties"
            />
            <!--  kiểu giá trị -->
            <DxColumn
              dataField="salaryValueType"
              caption="Kiểu giá trị"
              :minWidth="200"
              :maxWidth="360"
              alignment="left"
              :customize-text="formatValueTypeOption"
            />
            <!--  giá trị -->
            <DxColumn
              dataField="recipeSuggestion"
              caption="Giá trị"
              :minWidth="200"
              :maxWidth="360"
              :calculateCellValue="formatValue"
            />
            <!--  cột hover icons -->
            <DxColumn
              dataField="salaryCompositionId"
              caption=""
              :width="80"
              css-class="col-action"
              cell-template="idTemplate"
            >
            </DxColumn>
            <!-- hover xuất hiện icon -->
            <template #idTemplate="{ data }">
              <div
                class="floating-actions action-wrapper"
                @mouseleave="tooltipVisible = false"
              >
                <div
                  class="icon-actions"
                  @click="eventSelectedRow(data.data)"
                  @mouseenter="onTooltips($event, 'Đưa vào danh sách sử dụng')"
                >
                  <div class="icon-use"></div>
                </div>
              </div>
            </template>
          </DxDataGrid>
          <!--  tooltip  -->
          <MsTooltip
            :target="tooltipEvent"
            v-model:visible="tooltipVisible"
            :content="tooltipText"
          />
        </div>
        <!--  thanh cuối bản ghi (botton) -->
        <div class="paging">
          <div class="totalCount">
            <div class="sum_totalCount">
              {{ $t("paging.total") }}:
              <span class="total">{{ totalRecords }}</span>
            </div>
          </div>
          <div class="bottom-page">
            <div class="page-size-selector">
              <div class="text-dropdown-page">{{ $t("paging.pageSize") }}</div>

              <MsSelectBoxPaging
                :items="[15, 25, 50, 100]"
                v-model="pageSize"
                @update:modelValue="changePageSize"
              />
            </div>
            <!--  đếm bản ghi start- end  -->
            <div class="countStart_End">
              <div class="textNumber">
                <span class="countPaging">{{ pageLabel }}</span>
                {{ $t("paging.range") }}
              </div>
            </div>
            <div class="BoxPageClick">
              <div class="icon-pageLeft" @click="onClickLeft">
                <div
                  class="icon-page-left"
                  :class="{ disabled: isDisableLeft }"
                ></div>
              </div>
              <div class="icon-pageRight" @click="onClickRight">
                <div
                  class="icon-page-right"
                  :class="{ disabled: isDisableRight }"
                ></div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- modal thông báo -->
  <MsModal
    v-if="showModal"
    :salarySystemData="salarySystemData"
    @closeModal="showModal = false"
    @eventClick="eventClickAdd"
  />
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import { DxDataGrid, DxColumn, DxSelection } from "devextreme-vue/data-grid";
import SalarySystemService from "@/services/SalarySystemAPI";
import SalaryCompositionService from "@/services/salaryCompositionAPI";
import { activateToast } from "@/utils/toast.js";
import {
  componentTypeOptions,
  optionProperties,
  valueTypeOption,
} from "@/components/controls/Options.js";
import { useRouter } from "vue-router";
const router = useRouter();
const items = ref([]);
const pageIndex = ref(1);
const pageSize = ref(15);
const keyWord = ref("");
const status = ref(null);
const totalRecords = ref();
const showModal = ref();
const salarySystemData = ref();
const keyOrganization = ref([]);
///
/// điều khiển router
///
const goBack = () => {
  router.push("/salarycomposition");
};
///
/// tooltips
///
const tooltipEvent = ref(null);
const tooltipText = ref("");
const tooltipVisible = ref(false);

const onTooltips = (e, text) => {
  tooltipEvent.value = e.currentTarget;
  tooltipText.value = text;
  tooltipVisible.value = true;
};

///
///  phân trang
///
const paging = async () => {
  try {
    const paging = {
      PageIndex: pageIndex.value,
      PageSize: pageSize.value,
      Status: status.value,
      KeyWord: keyWord.value,
      KeyOrganization: keyOrganization.value,
    };
    const res = await SalarySystemService.paging(paging);
    // lấy list bản ghi
    items.value = res.data.items;
    // lấy tổng bản ghi
    totalRecords.value = res.data.totalRecords;
  } catch (err) {
    console.error("lỗi paging:", err);
  }
};

// cập nhật bản ghi khi lựa chọn
const changePageSize = () => {
  pageIndex.value = 1;
  paging();
};

// hiển thị bản ghi start --> end
const pageLabel = computed(() => {
  if (totalRecords.value === 0) return "0 - 0";
  const start = (pageIndex.value - 1) * pageSize.value + 1;
  const end = Math.min(start + pageSize.value - 1, totalRecords.value);
  return `${start}  -  ${end}`;
});

// nút Right
const isDisableRight = computed(() => {
  const totalPage = Math.ceil(totalRecords.value / pageSize.value);
  return pageIndex.value >= totalPage || totalRecords.value === 0;
});
const onClickRight = () => {
  const totalPage = Math.ceil(totalRecords.value / pageSize.value);
  if (pageIndex.value < totalPage) {
    pageIndex.value++;
    paging();
  }
};
// nút Left
const isDisableLeft = computed(() => {
  return pageIndex.value <= 1;
});
const onClickLeft = () => {
  if (pageIndex.value > 1) {
    pageIndex.value--;
    paging();
  }
};
///
/// tìm kiếm
///
const inputKeyWord = ref("");
const enter = async () => {
  keyWord.value = inputKeyWord.value;
  await paging();
};

///
/// hiển thị header trên bản ghi khi check box
///
const selectedId = ref([]);
const selectedRowsData = ref();
const countSelectRows = ref(0);
const onSelectionChanged = (e) => {
  // lấy tổng để hiển thị
  countSelectRows.value = e.selectedRowKeys.length;
  // lấy object
  selectedRowsData.value = e.selectedRowsData;
  // lấy id
  selectedId.value = selectedRowsData.value.map(
    (x) => x.salaryCompositionSystemId,
  );
};

///
/// thêm mới 1 bản ghi vào thành phần lương
///
const eventSelectedRow = (data) => {
  // mở modal
  showModal.value = true;
  // lấy bản ghi đã chọn
  salarySystemData.value = data;
};
const eventClickAdd = async () => {
  if (!salarySystemData.value) return;
  const data = salarySystemData.value;
  // gán dữ liệu vào key trùng BE
  const payload = {
    SalaryCompositionCode: data.salaryCompositionSystemCode,
    SalaryCompositionName: data.salaryCompositionSystemName,
    ComponentType: data.componentType,
    Nature: data.nature,
    SalaryValueType: data.salaryValueType,
    RecipeSuggestion: data.recipeSuggestion,
    // đơn vị áp dụng
    OrganizationUnitData: JSON.stringify([
      {
        organizationUnitId: "2a34b423-eed6-11f0-bd40-02508be6ec4f",
        organizationUnitName: "Công ty cổ phần MISA",
        organizationUnitParentId: null,
      },
    ]),
    //giá trị
    SalaryValue: "1",
    // tính chất
    nature: 3,
    SumValue: "1",
    ValueArrgegatitonScope: "1",
    Source: "Mặc định",
    Status: 1,
  };
  try {
    const res = await SalaryCompositionService.insert(payload);
    if (res && res.data?.success) {
      showModal.value = false;
      paging();
      activateToast("success", "Thêm thành công");
    } else {
      if (res && res.data?.userMsg) {
        activateToast("error", res.data?.userMsg);
      } else {
        activateToast("error", "Đã tồn tại trên hệ thống");
      }
    }
  } catch (error) {
    const data = error.response.data;
    if (data && data.userMsg) {
      activateToast("error", data.userMsg);
    } else {
      activateToast("error", "Đã tồn tại trên hệ thống");
    }
  }
};
///
/// hiển thị text lên UI trong bản ghi
///
// loại thành phần
const formatComponentType = (e) => {
  const options = componentTypeOptions.find((opt) => opt.value === e.value);
  return options ? options.label : "--";
};
// tính chất
const formatOptionProperties = (e) => {
  const options = optionProperties.find((opt) => opt.value === e.value);
  return options ? options.label : "--";
};
// kiểu giá trị
const formatValueTypeOption = (e) => {
  const options = valueTypeOption.find((opt) => opt.value === e.value);
  return options ? options.label : "--";
};
//giá trị
const formatValue = (e) => {
  return e.recipeSuggestion ? e.recipeSuggestion : "--";
};
onMounted(() => {
  paging();
});
</script>

<style scoped lang="scss">
.content {
  flex: 1;
  padding: 16px 24px;
  height: calc(100vh - 48px);
  width: calc(100% - 220px);
  background-color: #f2f2f2;

  .content-header {
    display: flex;
    justify-content: space-between !important;
    align-items: center;
    padding-bottom: 16px;

    .text-content-header {
      display: flex;
      justify-content: space-between !important;
      align-items: center;
      font-weight: 700;
      font-size: 20px;
      letter-spacing: 0.384px !important;

      .next-addSalary {
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

      .next-addSalary:hover {
        background-color: #cccccc;
      }
    }
  }
}

.content-main {
  height: calc(100% - 52px);
  width: 100%;
  position: relative;
  overflow: hidden;
  border-radius: 4px;

  .grid-filter {
    display: flex;
    align-items: center;
    padding: 12px 20px;
    background-color: #fff;
    position: relative;
    z-index: 8;
    height: 61px;
    justify-content: space-between;

    .search-header {
      display: flex;
      align-items: center;
      width: 300px;
      height: 36px;

      .input-search-options {
        display: flex;
        width: 100%;
        height: 100%;
        border: 1px solid #e0e0e0;
        border-radius: 4px;
        background: #fff;
        min-height: 28px;
        align-items: center;
        overflow: hidden;

        .icon-search-options-box {
          width: 24px;
          height: 24px;
          display: flex;
          align-items: center;
        }

        input {
          font-size: 14px !important;
          border: none;
          width: 100%;
          padding: 0 12px;
        }
      }

      .input-search-options:hover {
        border-color: #34b057;
      }
    }

    .options {
      display: flex;
      align-items: center;

      .statuses {
        display: flex;
        align-items: center;
        margin-right: 16px;
        cursor: pointer;
      }

      .filter {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 36px;
        height: 36px;
        border-radius: 4px;
        margin-right: 8px;
        background-color: #fff;
        border: 1px solid #ebebeb;
      }

      .filter:hover {
        border-color: #34b057;
        background-color: #eafff0;
        cursor: pointer;
      }

      .filter:hover .icon-filter {
        background-color: #34b057 !important;
        cursor: pointer;
      }

      .setting {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 36px;
        height: 36px;
        border-radius: 4px;
      }

      .setting:hover {
        background-color: #e5e5e5;
        cursor: pointer;
      }

      .setting:hover .icon-setting {
        background-color: #34b057 !important;
      }
    }
  }

  .grid-content {
    height: calc(100% - 61px);

    .salary {
      width: 100%;
      height: calc(100% - 60px);
      overflow: auto;
    }

    .paging {
      display: flex;
      align-items: center;
      justify-content: space-between;
      border-top: 1px solid #e0e0e0;
      padding: 10px 0;
      background: #f6f6f6;
      height: 60px;

      .totalCount {
        margin-left: 16px;
      }

      .bottom-page {
        display: flex;
        align-items: center;

        .page-size-selector {
          display: flex;
          align-items: center;

          .text-dropdown-page {
            margin-right: 8px;
          }
        }

        .countStart_End {
          padding: 0 24px;
          display: flex;
          align-items: center;
        }
      }

      .BoxPageClick {
        display: flex;
        margin-right: 16px;

        .icon-pageLeft,
        .icon-pageRight {
          display: flex;
          align-items: center;
          justify-content: center;
          width: 36px;
          height: 36px;
          border-radius: 50%;
        }
      }
    }
  }
}

.total,
.countPaging {
  color: #000;
  font-weight: 600;
  font-size: 14px;
}

.disabled {
  opacity: 0.5;
  pointer-events: none;
}

/* ===== DevExtreme ===== */

:deep(.dx-datagrid-headers .dx-datagrid-text-content) {
  font-weight: 700;
  font-size: 14px;
  color: #212121;
}

:deep(.dx-datagrid .dx-data-row td) {
  height: 36px;
  font-size: 14px;
  color: #212121;
}

:deep(.dx-checkbox-checked .dx-checkbox-icon) {
  background-color: #34b057 !important;
  border-radius: 4px;
}

:deep(.dx-datagrid-rowsview .dx-row.dx-selection > td) {
  background-color: #eafbf2 !important;
}

:deep(.dx-data-row:hover) td {
  background-color: #e7f6ef !important;
  cursor: pointer;
}

/* ===== style hover trên bản ghi ===== */

:deep(.col-action) {
  position: sticky !important;
  right: 0;
  z-index: 9;
}

.action-wrapper {
  opacity: 0;
  display: flex;
  justify-content: flex-end;
  transition: 0.15s;
  margin-right: 16px;
}

:deep(.dx-data-row:hover) .action-wrapper {
  opacity: 1;
}

:deep(.dx-data-row:hover .col-action) {
  background-color: #e7f6ef !important;
}

/* ===== Header khi chọn bản ghi ===== */

.grid-options {
  display: flex;
  align-items: center;
  padding: 12px 20px;
  background-color: #fff;
  height: 61px;

  .textNumber {
    font-weight: 600;
  }

  .text-label {
    color: rgb(52, 176, 87);
    cursor: pointer;
    font-weight: 700;
    margin: 0 16px;
  }

  .addToUseLis {
    height: 36px;
    border-radius: 4px;
    padding: 0 16px 0 12px;
    border: 1px solid #ebebeb;
    display: flex;
    align-items: center;
    margin-right: 8px;
    cursor: pointer;

    .iconAddToUseLis {
      width: 24px;
      height: 24px;
      margin-right: 4px;
      display: flex;
      justify-items: center;
      align-items: center;
    }
  }

  .addToUseLis:hover {
    color: #2ca64a;
    border-color: #2ca64a;
  }

  .addToUseLis:hover .icon-addToUseLis {
    background-color: #2ca64a !important;
  }
}
</style>
