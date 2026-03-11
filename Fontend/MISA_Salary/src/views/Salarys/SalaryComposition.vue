<template>
  <div class="content">
    <!-- header-box -->
    <div class="content-header">
      <div class="text-content-header">{{ $t("content.salaryTitle") }}</div>
      <div class="system-addition">
        <!--  danh mục của hệ thống -->
        <div class="system" @click="gotoSalarys">
          <div class="icon-system-box flex-center">
            <div class="icon-system"></div>
          </div>
          <div class="text-system">{{ $t("content.systemCategory") }}</div>
        </div>
        <div class="flex-center btn-add">
          <!-- thêm mới  -->
          <div class="addition" @click="gotoAddSalary">
            <div class="icon-addition-box">
              <div class="icon-addition"></div>
            </div>
            <div class="text-addition">{{ $t("content.addNew") }}</div>
          </div>
          <!-- thanh sọc ngắn các-->
          <div class="divider"></div>
          <!--  nút dropdown -->
          <div class="icon-dropdownAaddition" @click.stop="showSelectSystem">
            <div class="icon-dropdown-addition"></div>
            <!-- chọn từ danh mục hệ thống -->
            <div class="fromSystem_box" v-if="showDropSystem">
              <div class="fromSystem">
                <div class="salarySystem" @click="goToSystemModal">
                  <div class="textSystem">Chọn từ danh mục của hệ thống</div>
                </div>
              </div>
            </div>
          </div>
          <SalarySystemModal
            v-if="showSystemModal"
            @closeSystemModal="showSystemModal = false"
            @refreshData="paging"
          />
        </div>
      </div>
    </div>
    <!-- main-content(body) -->
    <div class="content-main">
      <!-- content(header in box) -->
      <div class="grid-filter" v-if="countSelectRows === 0">
        <!-- phần tìm kiếm -->
        <div class="search-header">
          <div class="input-search-options flex">
            <div class="icon-search-options-box" style="padding-left: 8px">
              <div class="icon-scale-search"></div>
            </div>
            <input
              type="text"
              v-model="inputKeyWord"
              @keyup.enter="enterFindKey"
              :placeholder="$t('filter.search')"
            />
          </div>
        </div>
        <!-- phần bên phải tìm kiếm trong header -->
        <div class="options">
          <!-- tất cả trạng thái -->
          <div class="statuses" @click="onMsDropDown">
            <div v-if="saveStatus === null">
              {{ $t("filter.allStatus") }}
            </div>
            <div v-else-if="saveStatus === 1">
              {{ $t("filter.onStatus") }}
            </div>
            <div v-else-if="saveStatus === 0">
              {{ $t("filter.offStatus") }}
            </div>
            <div
              class="icon-dropdown"
              style="margin-left: 8px; cursor: pointer"
            ></div>
          </div>
          <MsDropDown
            v-show="showMsDropDown"
            @selected="selectedMsDropDown"
            @close="showMsDropDown = false"
            v-model="saveStatus"
          />
          <!--  đơn vị tổ chức {{ $t("filter.allOrganization") }} -->
          <MsTagBox
            class="organization-unit"
            :items="unitTreeData"
            v-model="treeValue"
            @itemClick="selectedIdbyTree"
          />

          <!-- icon bộ lọc -->
          <div class="filter" @mouseenter="onTooltips($event, `Bộ lọc`)">
            <div class="icon-filter"></div>
          </div>
          <!-- icon setting -->
          <div class="setting" @mouseenter="onTooltips($event, `Thiết lập`)">
            <div class="icon-setting"></div>
          </div>
        </div>
      </div>
      <!--  tạo thêm hedear ( thay đổi khi lựa chọn checkbox) -->
      <div class="grid-options" v-else>
        <div>
          {{ $t("filter.selected") }}
          <span class="textNumber">{{ countSelectRows }}</span>
        </div>
        <div class="text-clearSelection">{{ $t("filter.deselected") }}</div>
        <!-- ngừng theo dõi -->
        <div
          class="stopfolow"
          v-if="followedIds != ``"
          @click="updateUnfollowedIds"
        >
          <div class="iconStopForllow">
            <div class="icon-StopFollow"></div>
          </div>
          <div class="text-StopFolow">{{ $t("filter.unfollowed") }}</div>
        </div>
        <!-- đang theo dỗi -->
        <div
          class="onfolow"
          v-if="unfollowedIds != ``"
          @click="updateFollowedIds"
        >
          <div class="iconOnForllow">
            <div class="icon-OnFollow"></div>
          </div>
          <div class="text-onFolow">{{ $t("filter.onfollowed") }}</div>
        </div>
        <!-- xóa -->
        <div class="clear" @click="onDeleteList()">
          <div class="boxIconClear">
            <div class="icon-clear"></div>
          </div>
          <div class="text-clear">{{ $t("filter.delete") }}</div>
        </div>
      </div>
      <!--  xử dụng thự viện dxDatagrid -->
      <div class="grid-content">
        <div class="salary">
          <DxDataGrid
            ref="gridRef"
            :height="'100%'"
            :data-source="items"
            no-data-text="Không có dữ liệu"
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
              dataField="salaryCompositionCode"
              caption="Mã thành phần"
              :min-width="200"
              :max-width="284"
            />
            <!--  tên thành phần -->
            <DxColumn
              dataField="salaryCompositionName"
              caption="Tên thành phần"
              :min-width="200"
              :max-width="284"
            />
            <!-- đơn vị áp dụng -->
            <DxColumn
              dataField="organizationUnitData"
              caption="Đơn vị áp dụng"
              :min-width="200"
              :max-width="284"
              :customize-text="formatOrganizationUnit"
            />
            <!--  loại thành phần -->
            <DxColumn
              dataField="componentType"
              caption="Loại thành phần"
              :min-width="200"
              :max-width="284"
              alignment="left"
              :customize-text="formatComponentType"
            />
            <!--  tính chất -->
            <DxColumn
              dataField="nature"
              caption="Tính chất"
              :min-width="200"
              :max-width="284"
              alignment="left"
              :customize-text="formatOptionProperties"
            />
            <!--  kiểu giá trị -->
            <DxColumn
              dataField="salaryValueType"
              caption="Kiểu giá trị"
              :min-width="200"
              :max-width="284"
              alignment="left"
              :customize-text="formatValueTypeOption"
            />
            <!--  giá trị -->
            <DxColumn
              dataField="recipeSuggestion"
              caption="Giá trị"
              :min-width="200"
              :max-width="284"
              cell-template="idValue"
            />
            <!-- style giá trị công thức -->
            <template #idValue="{ data }">
              <div
                class="formula-container"
                v-html="highlighter(data.value || '_ _')"
              ></div>
            </template>
            <!--  nguồn tạo -->
            <DxColumn
              dataField="source"
              caption="Nguồn tạo"
              :min-width="200"
              :max-width="284"
            />
            <!--  trạng thái -->
            <DxColumn
              dataField="status"
              caption="Trạng thái"
              :min-width="200"
              :max-width="284"
              alignment="center"
              cell-template="idStatus"
            />
            <!-- style trạng thái -->
            <template #idStatus="{ data }">
              <div class="confirmStatus">
                <div class="followStatus" v-if="data.value == 1">
                  <div class="iconStatus_on">
                    <div></div>
                  </div>
                  <div class="textOnStatus">{{ $t("content.following") }}</div>
                </div>
                <div class="stopStatus" v-if="data.value == 0">
                  <div class="iconStatus_stop">
                    <div></div>
                  </div>
                  <div class="textStopStatus">{{ $t("content.unfollow") }}</div>
                </div>
              </div>
            </template>
            <!--  cột hover icons -->
            <DxColumn
              dataField="salaryCompositionId"
              caption=""
              :width="160"
              css-class="col-action"
              cell-template="idTemplate"
            >
            </DxColumn>
            <template #idTemplate="{ data }">
              <div
                class="floating-actions action-wrapper"
                @mouseleave="tooltipVisible = false"
              >
                <!--  trạng thái -->
                <div
                  class="icon-actions"
                  @mouseenter="onTooltips($event, 'Trạng thái')"
                  @click="eventStatus(data.data)"
                >
                  <!--  icon ngừng theo dõi -->
                  <div
                    class="icon-offStatus"
                    v-if="data.data.status === 0"
                  ></div>
                  <!--  icon đang theo dõi -->
                  <div
                    class="icon-onStatus"
                    v-if="data.data.status === 1"
                  ></div>
                </div>
                <!-- nhân bản -->
                <div
                  class="icon-actions"
                  @mouseenter="onTooltips($event, 'Nhân bản')"
                  @click="eventCloning(data.data)"
                >
                  <div class="icon-AddComponent"></div>
                </div>
                <!--  sửa -->
                <div
                  class="icon-actions"
                  @mouseenter="onTooltips($event, 'Sửa')"
                  @click="editSalaryComposition(data.data)"
                >
                  <div class="icon-BaseSalary"></div>
                </div>
                <!--  xóa  -->
                <div
                  class="icon-actions"
                  @mouseenter="onTooltips($event, 'Xóa')"
                  @click="onDeleteRow(data.data)"
                >
                  <div class="icon-Delete"></div>
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
  <!-- MsModal -->
  <MsModal
    v-if="showMsModal"
    @onDeleteRow="receiveEventDelete"
    @eventClick="receiveEventStatus"
    @closeModal="showMsModal = false"
    :data="saveTextValue"
    :rowData="rowDeleteData"
    :rowStatusData="rowStatusData"
    :isListStatus="isListStatus"
  />
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import SalaryCompositionService from "@/services/salaryCompositionAPI";
import OrganizationService from "@/services/organizationAPI";
import { DxDataGrid, DxColumn, DxSelection } from "devextreme-vue/data-grid";
import { useRouter } from "vue-router";
import MsTagBox from "@/components/controls/MsTagBox.vue";
//modal salarySystem
import SalarySystemModal from "@/views/Salarys/SalarySystemModal.vue";
// toast thông báo
import { activateToast } from "@/utils/toast.js";
// import các options để hiển thị label lên UI
import {
  componentTypeOptions,
  optionProperties,
  valueTypeOption,
} from "@/components/controls/Options.js";
import salaryCompositionAPI from "../../services/salaryCompositionAPI";
const router = useRouter();
const items = ref([]);
const pageIndex = ref(1);
const pageSize = ref(15);
const keyWord = ref("");
const inputKeyWord = ref("");
const status = ref();
const totalRecords = ref(0);
const showMsDropDown = ref(false);
const saveStatus = ref(null);
const countSelectRows = ref(0);
const showMsModal = ref(false);
const saveTextValue = ref();
// đơn vị tổ chức
const keyOrganization = ref([]);
const treeValue = ref(null);
const unitTreeData = ref([]);

///
/// tooltip hiển thi
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
/// điều hướng router
///
const gotoAddSalary = () => {
  router.push("/add-salary");
};
const gotoSalarys = () => {
  router.push("/salary-system");
};
///
/// tìm kiếm
///
const enterFindKey = async () => {
  keyWord.value = inputKeyWord.value;
  await paging();
};

///
/// xóa bản ghi
///
//--- chọn 1 bản ghi
const rowDeleteData = ref();
const onDeleteRow = (data) => {
  showMsModal.value = true;
  rowDeleteData.value = data;
  saveTextValue.value = "single";
};
//-- chọn nhiều bản ghi
const onDeleteList = () => {
  showMsModal.value = true;
  saveTextValue.value = "multi";
};
// --- xóa bản ghi
const receiveEventDelete = async () => {
  if (rowDeleteData.value != null && saveTextValue.value === "single") {
    try {
      // lấy giá trị id
      const id = rowDeleteData.value.salaryCompositionId;
      // gọi hàm xóa trên BE
      const res = await SalaryCompositionService.delete(id);
      if (res.data || res) {
        showMsModal.value = false;
        activateToast("success", "Xóa thành công");
        paging();
      }
    } catch (error) {
      console.error("Lỗi delete:", error);
      showMsModal.value = false;
      activateToast("error", "Xóa thất bại");
    }
  } else if (
    listDataSelected.value != null &&
    saveTextValue.value === "multi"
  ) {
    try {
      showMsModal.value = true;
      // gọi hàm xóa trên BE
      const res = await SalaryCompositionService.deleteMany(
        listDataSelected.value,
      );
      if (res.data && res) {
        showMsModal.value = false;
        activateToast("success", "Xóa thành công");
        paging();
      }
    } catch (error) {
      console.error("Lỗi deleteByIds:", error);
      showMsModal.value = false;
      activateToast("error", "Xóa thất bại");
    }
  }
};

///
/// cập nhật bản ghi
///
const editSalaryComposition = (data) => {
  const id = data.salaryCompositionId;
  //router.push(`/add-salary/${id}`);
  router.push({ path: `/add-salary/${id}`, query: { mode: "editSalary" } });
};

///
/// nhân bản
///
const eventCloning = (data) => {
  const id = data.salaryCompositionId;
  //router.push(`/add-salary/${id}`);
  router.push({ path: `/add-salary/${id}`, query: { mode: "cloningSalary" } });
};

///
/// chuyển trạng thái
///
const rowStatusData = ref();
const eventStatus = async (e) => {
  if (e) {
    saveTextValue.value = "custom";
    showMsModal.value = true;
    rowStatusData.value = e;
  }
};
// nhận sự kiện click từ con( modal)
const receiveEventStatus = async () => {
  try {
    // nếu click trên bản ghi (1 trạng thái)
    if (saveTextValue.value === "custom") {
      const id = rowStatusData.value.salaryCompositionId;
      // lấy giá trị status
      var confirmStatus = rowStatusData.value.status;
      var status = ref();
      // đổi giá trị status
      if (id !== undefined && confirmStatus !== undefined) {
        if (confirmStatus === 1 && id !== 0) {
          status.value = 0;
        } else {
          status.value = 1;
        }
        const res = await salaryCompositionAPI.UpdateStatus(id, status.value);
        if (res && res.status === 200) {
          showMsModal.value = false;
          activateToast("success", "Cập nhật thành phần lương thành công");
          paging();
        }
      }
    } else if (
      saveTextValue.value === "listOffStatus" ||
      saveTextValue.value === "listOnStatus"
    ) {
      // cập nhật trạng thái theo list id
      // truyền list id và giá trị status
      if (saveTextValue.value === "listOffStatus") {
        var ids = followedIds.value;
        var status = 0;
        // gọi hàm cập nhật trên BE
        const res = await salaryCompositionAPI.UpdateStatusMany(ids, status);
        if (res && res.status) {
          showMsModal.value = false;
          activateToast("success", "Cập nhật thành công");
        }
      } else if (saveTextValue.value === "listOnStatus") {
        var ids = unfollowedIds.value;
        var status = 1;
        // gọi hàm cập nhật trên BE
        const res = await salaryCompositionAPI.UpdateStatusMany(ids, status);
        if (res && res.status) {
          showMsModal.value = false;
          activateToast("success", "Cập nhật thành công");
        }
      }
      paging();
    }
  } catch (error) {
    console.error("Lỗi dữ liệu trước khi gọi cập nhật:", error);
    activateToast("error", "Cập nhật thành phần lương thất bại");
  }
};

///
/// header (bị ẩn, click bản ghi sẽ hiện)
///
const listIdSelected = ref([]);
const listDataSelected = ref();
const followedIds = ref();
const unfollowedIds = ref();
const onSelectionChanged = (a) => {
  countSelectRows.value = a.selectedRowKeys.length;
  // lấy list id để hiển thị tổng
  listIdSelected.value = a.selectedRowKeys;
  listDataSelected.value = listIdSelected.value.map(
    (x) => x.salaryCompositionId,
  );
  // lấy id có status = 1
  followedIds.value = listIdSelected.value
    .filter((x) => x.status == 1)
    .map((x) => x.salaryCompositionId);
  // lấy id có status = 0
  unfollowedIds.value = listIdSelected.value
    .filter((x) => x.status == 0)
    .map((x) => x.salaryCompositionId);
};
// sự kiện click thay đổi trạng thái thei list id
const updateUnfollowedIds = () => {
  showMsModal.value = true;
  saveTextValue.value = "listOffStatus";
};
const updateFollowedIds = () => {
  showMsModal.value = true;
  saveTextValue.value = "listOnStatus";
};

///
/// tất cả trạng thái
///
const onMsDropDown = () => {
  showMsDropDown.value = !showMsDropDown.value;
};
const selectedMsDropDown = async (statusValue) => {
  saveStatus.value = statusValue; // lưu trạng thái lại
  status.value = statusValue; // lưu vào status
  try {
    await paging();
  } catch (error) {
    console.error("lỗi paging:", error);
  }
  showMsDropDown.value = false;
};

///
///  đơn vị tổ chức ( cây tree )
///

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
    }
  } catch (error) {
    consolelog("Lỗi lấy data bảng cơ cấu tổ chức:", error);
  }
};
// tìm kiếm bản ghi theo tree
const selectedIdbyTree = async (e) => {
  try {
    if (e) {
      // lấy tất cả [ id ]
      const getIdsBytree = e.map((item) => {
        return item.organizationUnitId;
      });
      keyOrganization.value = getIdsBytree;
      await paging();
    }
  } catch (error) {
    console.log("Lỗi tìm kiếm bản ghi theo tree:", error);
  }
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
    const res = await SalaryCompositionService.paging(paging);
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
/// chọn từ danh mục hệ thống
///
const showDropSystem = ref(false);
const showSelectSystem = () => {
  showDropSystem.value = !showDropSystem.value;
};
// mở salarysystemMODal
const showSystemModal = ref(false);
const goToSystemModal = () => {
  showSystemModal.value = !showSystemModal.value;
};
///
///  hiển thị dữ liệu trên form client
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
// đơn vị áp dụng
const formatOrganizationUnit = (e) => {
  try {
    const datalist = JSON.parse(e.value || "[]");
    // lấy list id
    const datalistId = datalist.map((e) => {
      return e.organizationUnitId;
    });
    // loại bỏ thằng con
    const removeChildId = datalist.filter((e) => {
      const dataParentId = datalistId.includes(e.organizationUnitParentId);
      return !dataParentId;
    });
    if (removeChildId && removeChildId.length > 0) {
      const showText = removeChildId.map((item) => item.organizationUnitName);
      return showText.join(", ");
    } else {
      return "--";
    }
  } catch (error) {
    console.error("Lỗi json:", error);
  }
};
// giá trị
// phủ màu:
const formulaLanguage = {
  keyword: /\b(SUM|IF|DATE|IN|OR|MONTH)\b/gi,
  variable: [/\[.*?\]/, /\b[a-zA-ZÀ-ỹ]+\b/],
  number: /\b\d+(\.\d+)?\b/,
  operator: /[+\-*/%()=,]/,
};

const highlighter = (code) => {
  return Prism.highlight(code || "", formulaLanguage, "javascript");
};

///
/// khi vào trang sẽ loading
///
onMounted(async () => {
  await paging();
  await loadOrganizationData();
});
</script>

<style scoped lang="scss">
.content {
  padding: 16px 24px;
  height: calc(100vh - 48px);
  background-color: #f2f2f2;
  flex: 1;
  overflow-y: auto;
  overflow-x: visible;

  ///
  /// header
  ///
  .content-header {
    display: flex;
    justify-content: space-between !important;
    align-items: center;
    padding-bottom: 16px;

    .text-content-header {
      font-weight: 700;
      font-size: 20px;
      letter-spacing: 0.384px !important;
    }

    .system-addition {
      display: flex;
      align-items: center;
      cursor: pointer;
      position: relative;
      //danh mục của hệ thống
      .system {
        display: flex;
        align-items: center;
        height: 36px;
        width: auto;
        border-radius: 4px;
        padding: 0px 16px;
        background-color: #fff;
        color: #212121;
        border: 1px solid #ebebeb;
        margin-right: 8px;
        font-size: 14px;
        cursor: pointer;

        .text-system {
          white-space: nowrap;
          font-weight: 500;
          font-size: 14px;
        }

        .icon-system-box {
          width: 24px;
          height: 24px;
        }
      }

      //hover danh mục của hệ thống
      .system:hover {
        border-color: #2ca64a;
        color: #2ca64a;
      }

      .system:hover .icon-system {
        background-color: #2ca64a !important;
      }

      .btn-add {
        border-radius: 4px;
        background-color: #34b057;

        .text-addition {
          font-weight: 500;
        }
      }

      .addition {
        display: flex;
        align-items: center;
        width: auto !important;
        height: 36px;
        padding: 0px 16px 0px 12px;
        color: #fff;

        .icon-addition-box {
          display: flex;
          justify-content: center;
          align-items: center;
          margin-right: 4px;
          width: 24px;
          height: 24px;
        }
      }
    }

    //thanh sọc
    .divider {
      width: 1px;
      height: 21px;
      background-color: #fff;
    }
    // chọn từ danh mục hệ thống
    .icon-dropdownAaddition {
      display: flex;
      height: 36px;
      width: 36px;
      justify-content: center;
      align-items: center;
      border-radius: 0px 4px 4px 0px;
      padding: 6px 5px;
      background-color: #34b057;
      position: relative;

      .fromSystem_box {
        position: absolute;
        display: flex;
        margin-top: 10px;
        top: 100%;
        left: 0;
        z-index: 999;
        white-space: nowrap;
        height: 53px;
        left: -204px;
        border-radius: 4px;
        box-shadow: 0 4px 16px rgba(0, 0, 0, 0.15);
        cursor: pointer;

        .fromSystem {
          padding: 8px 6px;
          background-color: #fff;
          border-radius: 4px;
          display: flex;
          justify-items: center;
          align-items: center;
          .salarySystem {
            height: 40px;
            display: flex;
            justify-items: center;
            align-items: center;
            border-radius: 4px;
            .textSystem {
              margin: 0px 6px;
            }
          }
          .salarySystem:hover {
            background-color: #e7f6ef;
          }
        }
      }
    }
  }
}

///
/// main- content(body)
///
.content-main {
  height: calc(100% - 52px);
  width: 100%;
  position: relative;
  overflow: hidden;
  border: none;
  border-radius: 4px;

  //content(header in box)
  .grid-filter {
    display: flex;
    align-items: center;
    padding: 12px 20px;
    background-color: #fff;
    position: relative;
    z-index: 8;
    height: 61px;
    justify-content: space-between;

    //phần tìm kiếm
    .search-header {
      height: 100%;
      width: 300px;
      margin-right: 12px;
      display: flex;
      align-items: center;
      justify-content: space-between;

      .input-search-options {
        height: 100%;
        display: flex;
        width: 100%;
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
          align-items: center !important;
          justify-content: space-between;
        }

        input {
          font-size: 14px !important;
          height: 100%;
          border: none;
          width: 100%;
          padding: 0 12px;
        }
      }

      //hover thanh tìm kiếm
      .input-search-options:hover {
        border-color: #34b057;
      }
    }

    //phần options bên phải tìm kiếm
    .options {
      display: flex;
      align-items: center;
      position: relative;

      //tất cả trạng thái
      .statuses {
        display: flex;
        justify-content: center;
        align-items: center;
        margin-right: 16px;
        cursor: pointer;
        position: relative;
        white-space: nowrap;

        .icon-statuses {
          margin-left: 8px;
          cursor: pointer;
        }
      }
      //bộ lọc-filter
      .filter {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 36px;
        height: 36px;
        border-radius: 4px;
        padding: 6px 5px;
        margin-right: 8px;
        font-size: 14px;
        background-color: #fff;
        color: #212121;
        border: 1px solid #ebebeb;
      }

      /* Hover bộ lọc*/
      .filter:hover {
        border-color: #34b057;
        background-color: #eafff0;
      }

      .filter:hover .icon-filter {
        background-color: #34b057 !important;
      }

      //setting
      .setting {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 36px;
        height: 36px;
        border-radius: 4px;
        padding: 6px 5px;
        min-width: 0px;
      }

      //hover setting
      .setting:hover {
        background-color: #e5e5e5;
      }

      .setting:hover .icon-setting {
        background-color: #34b057 !important;
      }
    }
  }

  .grid-content {
    height: calc(100% - 61px);

    ///
    /// phần list salary
    ///
    .salary {
      position: relative;
      width: 100%;
      height: calc(100% - 60px);
      overflow-x: auto;
      overflow-y: auto;
      display: block;
    }

    //thah cuối bản ghi
    .paging {
      display: flex;
      align-items: center;
      justify-content: space-between;
      border-top: 1px solid #e0e0e0;
      padding: 10px 0;
      background: #f6f6f6;
      height: 60px;
      z-index: 8;

      .totalCount {
        display: flex;
        line-height: 40px;
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

          .select-dropdown-page {
            background-color: #34b057;
            height: 26px;
            width: 40px;
          }
        }

        .countStart_End {
          padding: 0 24px;
          height: 40px;
          display: flex;
          justify-content: center;
          align-items: center;
        }
      }

      .BoxPageClick {
        width: 70px;
        height: 40px;
        display: flex;
        margin-right: 16px;
        cursor: pointer;

        .icon-pageLeft,
        .icon-pageRight {
          cursor: pointer;
          color: #afb3c1;
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

///
///  style trạng thái hoạt động
///
.followStatus {
  gap: 8px;
  display: flex;
  justify-content: center;
  align-items: center;
  .textOnStatus {
    color: rgb(52, 176, 87);
  }
  .iconStatus_on {
    height: 8px;
    width: 8px;
    border-radius: 50%;
    background-color: rgb(52, 176, 87);
  }
}
.stopStatus {
  gap: 8px;
  display: flex;
  justify-content: center;
  align-items: center;
  .textStopStatus {
    color: rgb(255, 153, 0);
  }
  .iconStatus_stop {
    height: 8px;
    width: 8px;
    border-radius: 50%;
    background-color: rgb(255, 153, 0);
  }
}

///
/// style font paging
///
.total {
  padding: 0px 6px;
  color: #000;
  font-weight: 600;
  font-size: 14px;
}

.countPaging {
  color: #000;
  font-weight: 600;
  font-size: 14px;
}

.disabled {
  opacity: 0.5;
  pointer-events: none;
}

///
/// style trong thư viện
///
/* Header */
:deep(.dx-datagrid-text-content) {
  width: 100% !important;
}

:deep(.dxdx-datagrid) {
  height: 36px;
}

:deep(.dx-datagrid .dx-header-row) {
  background-color: #f2f2f2 !important;
}

:deep(.dx-datagrid-headers .dx-datagrid-text-content) {
  font-weight: 700 !important;
  font-size: 14px;
  color: #212121 !important;
}

/* Body */
:deep(.dx-datagrid .dx-data-row td) {
  height: 36px !important;
  font-size: 14px !important;
  color: #212121 !important;
  max-width: 284px;
}

/* Đổi màu nền khi checkbox được tick */
:deep(.dx-checkbox-checked .dx-checkbox-icon) {
  background-color: #34b057 !important;
  color: #fff;
  border-radius: 4px;
}

/* Dòng được chọn (checkbox tích vào) */
:deep(.dx-datagrid-rowsview .dx-row.dx-selection > td) {
  background-color: #eafbf2 !important;
}
// hover màu trên bản ghi
:deep(.dx-data-row:hover) td {
  background-color: #e7f6ef !important;
  cursor: pointer;
}

///
/// hover trên bản ghi
///
:deep(.pin-icon) {
  opacity: 0;
  transition: 0.15s;
  margin-left: auto;
  cursor: pointer;
}

:deep(.dx-header-row td:hover .pin-icon) {
  opacity: 1;
}

:deep(.col-action) {
  position: sticky !important;
  right: 0;
  //z-index: 9;
}

/* Ẩn mặc định */
.action-wrapper {
  opacity: 0;
  position: relative;
  display: flex;
  align-items: center;
  justify-content: flex-end;
  transition: 0.15s;

  .icon-actions {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 40px;
  }
}

/* Hover vào row thì show icon */
:deep(.dx-data-row:hover) .action-wrapper {
  opacity: 1;
  pointer-events: auto !important;
}

:deep(.dx-data-row:hover .col-action) {
  background-color: #e7f6ef !important;
}

///
/// style header ( header ẩn , hiện khi chọn bản ghi)
///
.grid-options {
  display: flex;
  align-items: center;
  padding: 12px 20px;
  background-color: #fff;
  position: relative;
  z-index: 8;
  height: 61px;

  .textNumber {
    color: #000;
    font-weight: 600;
    font-size: 14px;
  }

  .text-clearSelection {
    color: rgb(52, 176, 87);
    cursor: pointer;
    font-weight: 700;
    margin-right: 16px !important;
    margin-left: 16px !important;
  }
  //ngừng theo dõi
  .stopfolow {
    width: auto !important;
    height: 36px !important;
    border-radius: 4px;
    padding: 0px 16px 0px 12px;
    min-width: 0px !important;
    background-color: #fff;
    color: #f90;
    border: 1px solid #f90;
    font-size: 14px;
    display: flex;
    align-items: center;
    justify-content: center;
    margin-right: 8px;
    cursor: pointer;

    .iconStopForllow {
      width: 24px;
      height: 24px;
      display: flex;
      align-items: center;
      justify-content: center;
      margin-right: 4px;
    }
  }
  // đang theo dõi
  .onfolow {
    width: auto !important;
    height: 36px !important;
    border-radius: 4px;
    padding: 0px 16px 0px 12px;
    min-width: 0px !important;
    background-color: #fff;
    color: #34b057;
    border: 1px solid #34b057;
    font-size: 14px;
    display: flex;
    align-items: center;
    justify-content: center;
    margin-right: 8px;
    cursor: pointer;

    .iconOnForllow {
      width: 24px;
      height: 24px;
      display: flex;
      align-items: center;
      justify-content: center;
      margin-right: 4px;
    }
  }

  .clear {
    display: flex;
    align-items: center;
    justify-content: center;
    width: auto !important;
    height: 36px !important;
    border-radius: 4px;
    padding: 0px 16px 0px 12px;
    min-width: 0px !important;
    background-color: #fff;
    color: #ef292f;
    border: 1px solid #ef292f;
    cursor: pointer;

    .boxIconClear {
      margin-right: 4px;
      width: 24px;
      height: 24px;
      display: flex;
      justify-content: center;
      align-items: center;
    }

    .text-clear {
      color: #ff6161;
    }
  }

  .clear:hover {
    background-color: #f6e3e3;
  }
}

///
/// Đơn vị tổ chức
///
.organization-unit {
  width: 350px;
  margin-right: 8px;
}
:deep(.organization-unit .dropdown-panel) {
  width: 375px !important;
}
///
/// style màu font chữ nhập trong thư viện
///
:deep(.token.keyword) {
  color: black;
  font-weight: 500;
}

:deep(.token.variable) {
  color: blue;
  font-weight: 500;
}

:deep(.token.operator) {
  color: red;
  font-weight: 500;
}

:deep(.token.number) {
  color: black;
  font-weight: 500;
}
</style>
