<template>
  <div class="main_modalSystem">
    <div class="modalSystem">
      <!-- header -->
      <div class="header">
        <div class="textLabel">Thêm từ danh mục hệ thống</div>
        <div class="box_iconEdit" @click="closeSystemModal">
          <div class="icon_editSystemModal"></div>
        </div>
      </div>
      <!-- content -->
      <div class="content-main">
        <!-- content(header in box) -->
        <div class="grid-filter">
          <!-- phần tìm kiếm -->
          <div class="search-header">
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
              >
              </DxColumn>
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
                <div class="text-dropdown-page">
                  {{ $t("paging.pageSize") }}
                </div>

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
                <!-- Left -->
                <div class="icon-pageLeft" @click="onClickLeft">
                  <div
                    class="icon-page-left"
                    :class="{ disabled: isDisableLeft }"
                  ></div>
                </div>

                <!-- Right -->
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
      <!--  bottom -->
      <div class="modal-footer">
        <div class="btn-edit" @click="closeSystemModal">
          <div class="cancel">Hủy</div>
        </div>
        <!-- đồng ý -->
        <div
          class="btn"
          @click="eventAddSystem"
          :class="{ active: selectedRowsData.length <= 0 }"
        >
          <div>Đồng ý</div>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup>
import { ref, onMounted, computed } from "vue";
import { DxDataGrid, DxColumn, DxSelection } from "devextreme-vue/data-grid";
import SalarySystemService from "@/services/SalarySystemAPI";
import SalaryCompositionService from "@/services/salaryCompositionAPI.js";
import {
  componentTypeOptions,
  optionProperties,
  valueTypeOption,
} from "@/components/controls/Options.js";
import { activateToast } from "@/utils/toast.js";
const items = ref([]);
const pageIndex = ref(1);
const pageSize = ref(15);
const keyWord = ref("");
const status = ref(null);
const totalRecords = ref();

/// gửi lên cha
const emit = defineEmits(["closeSystemModal", "[refreshData]"]);
const closeSystemModal = () => {
  emit("closeSystemModal");
};

///
/// tooltips
///
const tooltipEvent = ref(null);
const tooltipText = ref("");
const tooltipVisible = ref(false);

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
/// chọn bản ghi trên form
///
const selectedRowsData = ref([]);

const onSelectionChanged = (e) => {
  // lấy object
  selectedRowsData.value = e.selectedRowsData;
};
// sự kiện thêm mới bản ghi
const eventAddSystem = async () => {
  if (!selectedRowsData.value) return;
  // chuyển sang thành object
  const selectRowObject = selectedRowsData.value[0];
  //gán vào data để máp
  const data = selectRowObject;
  // gán dữ liệu vào các trường dữ liệu tương ứng BE
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
  const res = await SalaryCompositionService.insert(payload);
  if (res && res.data?.success) {
    emit("closeSystemModal");
    emit("refreshData");
    activateToast("success", "Thêm thành công");
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

// giá trị
const formatValue = (e) => {
  return e.recipeSuggestion ? e.recipeSuggestion : "--";
};

onMounted(() => {
  paging();
});
</script>
<style scoped lang="scss">
.main_modalSystem {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: 999;
  background: rgba(0, 0, 0, 0.4);
  display: flex;
  align-items: center;
  justify-content: center;

  .modalSystem {
    display: flex;
    flex-direction: column;
    margin: 10px;
    border-radius: 4px;
    background-color: #fff;
    box-shadow: 0 4px 16px rgba(0, 0, 0, 0.15);
    min-width: 700px;
    width: 75%;
    height: 80%;

    .header {
      padding: 24px 24px 0 24px;
      width: 100%;

      display: flex;
      align-items: center;
      justify-content: space-between;

      .textLabel {
        font-weight: 700;
        font-size: 20px;
        color: rgb(33, 33, 33);
      }

      .box_iconEdit {
        height: 36px;
        width: 36px;

        display: flex;
        align-items: center;
        justify-content: center;
      }
    }

    /* CONTENT */

    .content-main {
      flex: 1;
      display: flex;
      flex-direction: column;

      overflow: hidden;

      padding: 24px;
      box-sizing: border-box;

      .grid-filter {
        display: flex;
        align-items: center;
        padding: 12px;
        background-color: #fff;
        height: 61px;

        .search-header {
          display: flex;
          align-items: center;
          margin-right: 24px;
          width: 300px;
          height: 36px;

          .input-search-options {
            display: flex;
            align-items: center;
            height: 100%;
            width: 100%;
            border: 1px solid #e0e0e0;
            border-radius: 4px;
            background: #fff;
            min-height: 28px;
            overflow: hidden;

            .icon-search-options-box {
              width: 24px;
              height: 24px;
              display: flex;
              align-items: center;
            }

            input {
              font-size: 14px;
              border: none;
              padding: 0 12px;
              width: 100%;
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
            cursor: pointer;
          }
        }
      }

      /* GRID */

      .grid-content {
        flex: 1;
        display: flex;
        flex-direction: column;
        overflow: hidden;

        .salary {
          flex: 1;
          width: 100%;
          overflow: auto;
        }

        /* PAGING */

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

    /* FOOTER */

    .modal-footer {
      padding: 16px 24px;
      width: 100%;

      display: flex;
      justify-content: flex-end;

      background: #f2f2f2;
      border-radius: 0 0 4px 4px;

      .btn-edit {
        width: auto !important;
        height: 36px !important;

        border-radius: 4px;
        padding: 0px 16px;
        min-width: 80px !important;

        overflow: hidden;

        background-color: #fff;
        color: #212121;
        border: 1px solid #ebebeb;

        display: flex;
        justify-content: center;
        align-items: center;

        margin-right: 8px;

        &:hover {
          border-color: #2ca64a;
          color: #2ca64a;
          cursor: pointer;
        }
      }

      .btn {
        width: auto !important;
        height: 36px !important;

        border-radius: 4px;
        padding: 0px 16px;
        min-width: 80px !important;

        background-color: #34b057;
        color: #fff;
        border: 1px solid #34b057;

        font-size: 14px;

        display: flex;
        justify-content: center;
        align-items: center;

        cursor: pointer;
      }
    }
  }
}

/* TEXT */

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
// sự kiện bottom
.active {
  background-color: #93d1a5 !important;
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

:deep(.col-action) {
  position: sticky !important;
  right: 0;
  z-index: 9;
}
</style>
