<template>
  <div class="main-menu-sidebar" :class="{ disabled: isCollapsed }">
    <div class="sidebar--items">
      <div class="scroll-area--main-sidebar">
        <!-- tổng quan -->
        <div
          class="sidebar--item w-full justify-between"
          @click="onClickSidebar(MsEnum.MenuSidebar.Overview)"
          :class="{ active: sidebarActive == MsEnum.MenuSidebar.Overview }"
        >
          <div class="flex-start-center w-full">
            <div class="sidebar-icon sidebar-dashboard"></div>
            <div class="truncate">{{ $t("sidebar.overview") }}</div>
          </div>
        </div>
        <!--  thành phần lương  -->
        <div
          class="sidebar--item w-full justify-between"
          @click="onClickSidebar(MsEnum.MenuSidebar.SalaryComponent)"
          :class="{
            active: sidebarActive == MsEnum.MenuSidebar.SalaryComponent,
          }"
        >
          <div class="flex-start-center w-full">
            <div class="sidebar-icon sidebar-salary-composition"></div>
            <div class="truncate">{{ $t("sidebar.salaryComponent") }}</div>
          </div>
        </div>
        <!--  mẫu bảng lương  -->
        <div
          class="sidebar--item w-full justify-between"
          @click="onClickSidebar(MsEnum.MenuSidebar.SalaryTemplate)"
          :class="{
            active: sidebarActive == MsEnum.MenuSidebar.SalaryTemplate,
          }"
        >
          <div class="flex-start-center w-full">
            <div class="sidebar-icon sidebar-salary-template"></div>
            <div class="truncate">{{ $t("sidebar.salaryTemplate") }}</div>
          </div>
        </div>
        <!--  dữ liệu tính lương  -->
        <div
          class="sidebar-group"
          @click="onClickSidebar(MsEnum.MenuSidebar.SalaryData)"
        >
          <div
            class="group-header w-full"
            :class="{ active: sidebarActive == MsEnum.MenuSidebar.SalaryData }"
          >
            <div class="flex-start-center w-full justify-between">
              <div class="flex-start-center w-full">
                <div class="sidebar-icon sidebar-salary-data"></div>
                <div class="truncate">{{ $t("sidebar.salaryData") }}</div>
              </div>
              <div class="sidebar-dropdown"></div>
            </div>
          </div>
        </div>
        <!--  tính lương -->
        <div
          class="sidebar-group"
          @click="onClickSidebar(MsEnum.MenuSidebar.SalaryCalculation)"
        >
          <div
            class="group-header w-full"
            :class="{
              active: sidebarActive == MsEnum.MenuSidebar.SalaryCalculation,
            }"
          >
            <div class="flex-start-center w-full justify-between">
              <div class="flex-start-center w-full">
                <div class="sidebar-icon sidebar-salary-table"></div>
                <div class="truncate">{{ $t("sidebar.salaryCalculate") }}</div>
              </div>
              <div class="sidebar-dropdown"></div>
            </div>
          </div>
        </div>
        <!--  chi trả  -->
        <div
          class="sidebar-group"
          @click="onClickSidebar(MsEnum.MenuSidebar.Payment)"
        >
          <div
            class="group-header w-full"
            :class="{ active: sidebarActive == MsEnum.MenuSidebar.Payment }"
          >
            <div class="flex-start-center w-full justify-between">
              <div class="flex-start-center w-full">
                <div class="sidebar-icon sidebar-payment"></div>
                <div class="truncate">{{ $t("sidebar.payment") }}</div>
              </div>
              <div class="sidebar-dropdown"></div>
            </div>
          </div>
        </div>
        <!--  báo cáo  -->
        <div
          class="sidebar-group"
          @click="onClickSidebar(MsEnum.MenuSidebar.Report)"
        >
          <div
            class="group-header w-full"
            :class="{ active: sidebarActive == MsEnum.MenuSidebar.Report }"
          >
            <div class="flex-start-center w-full justify-between">
              <div class="flex-start-center w-full">
                <div class="sidebar-icon sidebar-report"></div>
                <div class="truncate">{{ $t("sidebar.report") }}</div>
              </div>
              <div class="sidebar-dropdown"></div>
            </div>
          </div>
        </div>
        <!--  thiết lập  -->
        <div
          class="sidebar-group"
          @click="onClickSidebar(MsEnum.MenuSidebar.Settings)"
        >
          <div
            class="group-header w-full"
            :class="{ active: sidebarActive == MsEnum.MenuSidebar.Settings }"
          >
            <div class="flex-start-center w-full justify-between">
              <div class="flex-start-center w-full">
                <div class="sidebar-icon sidebar-setting"></div>
                <div class="truncate">{{ $t("sidebar.settings") }}</div>
              </div>
              <div class="sidebar-dropdown"></div>
            </div>
          </div>
        </div>
      </div>
      <div class="sidebar-toggle-tooltip flex-center" @click="eventToggle">
        <div class="button-toggle flex-center jus-center">
          <div class="toggle-icon"></div>
          <div class="toggle-text">{{ $t("sidebar.collapse") }}</div>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup>
import { ref } from "vue";
import MsEnum from "@/enums/enums.js";
import { useRouter } from "vue-router";
const router = useRouter();
const sidebarActive = ref(MsEnum.MenuSidebar.SalaryComponent);
///
/// click từng tính năng trên sidebar
/// dựa theo enums
///
const onClickSidebar = (e) => {
  sidebarActive.value = e;
  if (e == MsEnum.MenuSidebar.SalaryComponent) {
    router.push("/salarycomposition");
  } else {
    router.push("/featureDevelopment");
  }
};
///
/// thu gọn
///
const isCollapsed = ref(false);
const eventToggle = () => {
  isCollapsed.value = !isCollapsed.value;
};
</script>

<style scoped lang="scss">
.h-36 {
  height: 36px;
}

.w-full {
  width: 100%;
}

//
// sidebar
//
.main-menu-sidebar {
  width: 220px;
  height: calc(100vh - 48px);
  position: relative;
  background: #161a17;
  z-index: 9;
  overflow: hidden;

  &::before {
    content: "";
    display: block;
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-image: var(--bg_sidebar);
    background-repeat: no-repeat;
    background-position: bottom;
    background-size: contain;
    z-index: 10;
    pointer-events: none;
    opacity: 0.4;
  }

  .sidebar--items {
    padding-top: 17px;
    height: 100%;
    width: 100%;

    .scroll-area--main-sidebar {
      width: 100%;
      box-sizing: border-box;
      margin: 0 !important;
      padding: 0 12px !important;
      position: relative;
      height: calc(100vh - 121px);

      //phần sidebar - 3 button đầu tiên
      .sidebar--item {
        height: 36px;
        padding: 8px 12px;
        border-radius: 8px;
        cursor: pointer;
        margin: 8px 0px;
      }

      .sidebar--item:not(.active):hover {
        background-color: #34b05740;
        color: red;
      }

      //font chữ trong sidebar
      .truncate {
        color: #babfc2;
        line-height: 16px;
        font-weight: 400;
      }

      //icon và nút phải
      .sidebar-icon {
        min-width: 20px;
        margin-right: 8px;
      }

      .sidebar-dropdown {
        margin-right: 4px;
      }

      //5 button còn lại
      .sidebar-group {
        margin: 4px -18px 4px -12px;
        padding: 0 12px;
        padding-right: 18px;

        //phần sidebar button có  drop
        .group-header {
          height: 36px;
          font-size: 1rem;
          padding: 8px 12px;
          margin: 8px 0;
          cursor: pointer;
          border-radius: 8px;
          transition: none;
          opacity: unset;
          display: flex;
          align-items: center;
        }

        .group-header:not(.active):hover {
          background-color: #34b05740;
          color: #fff;
        }
      }
    }

    //sidebar-toggle
    .sidebar-toggle-tooltip {
      display: flex;
      justify-content: center;

      .button-toggle {
        width: 176px;
        height: 36px;
        background: hsla(0, 0%, 100%, 0.08);
        border-radius: 8px;
        padding: 0 12px;
        cursor: pointer;
        transition: all 0.3s ease;
      }
      .toggle-icon {
        min-width: 16px;
        margin-right: 8px;
      }

      .toggle-text {
        color: #c1c1c1;
      }
      // hover icon bottom
      .button-toggle:hover .toggle-text {
        color: #fff !important;
      }
      .button-toggle:hover .toggle-icon {
        background-color: #fff !important;
      }
    }
  }
}

///
/// ẩn hiện sidebar
///
.main-menu-sidebar.disabled {
  width: 70px !important;
}
.main-menu-sidebar.disabled .truncate {
  opacity: 0 !important;
  width: 0 !important;
  padding: 0 !important;
  margin: 0 !important;
  overflow: hidden !important;
  white-space: nowrap !important;
}

.main-menu-sidebar.disabled .sidebar-dropdown {
  opacity: 0 !important;
  width: 0 !important;
  padding: 0 !important;
  margin: 0 !important;
  overflow: hidden !important;
  white-space: nowrap !important;
}
.main-menu-sidebar.disabled .toggle-text {
  opacity: 0 !important;
  width: 0 !important;
  padding: 0 !important;
  margin: 0 !important;
  overflow: hidden !important;
  white-space: nowrap !important;
}

///
/// active cho từng tính năng
///
.sidebar--item.active {
  background-color: #3bb35d;
}
.sidebar--item.active .truncate {
  color: #fff !important;
}
.sidebar--item.active .sidebar-icon {
  background-color: #fff !important;
}
//
.group-header.active {
  background-color: #3bb35d;
}
.group-header.active .truncate {
  color: #fff !important;
}
.group-header.active .sidebar-icon {
  background-color: #fff !important;
}
.group-header.active .sidebar-dropdown {
  background-color: #fff !important;
}
///
/// hover từng tính năng trên sidebar
///
.sidebar--item:hover .truncate {
  color: #fff !important;
}
.sidebar--item:hover .sidebar-icon {
  background-color: #fff !important;
}
.group-header:hover .truncate {
  color: #fff !important;
}
.group-header:hover .sidebar-icon {
  background-color: #fff !important;
}
.group-header:hover .sidebar-dropdown {
  background-color: #fff !important;
}
</style>
