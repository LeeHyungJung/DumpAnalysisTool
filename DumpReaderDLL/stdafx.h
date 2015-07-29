// stdafx.h : 자주 사용하지만 자주 변경되지는 않는
// 표준 시스템 포함 파일 및 프로젝트 관련 포함 파일이
// 들어 있는 포함 파일입니다.
//

#pragma once

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             // 거의 사용되지 않는 내용은 Windows 헤더에서 제외합니다.
// Windows 헤더 파일:
#include <windows.h>
#include <DbgHelp.h>
#include <iostream>
#include <stdint.h>
#include <assert.h>
#include <string>
#include <memory>
#include <vector>
#include <map>
#include <algorithm>
#include <assert.h>
#include <objbase.h>

#define _CRTDBG_MAP_ALLOC
#include <stdlib.h>
#include <crtdbg.h>

using namespace std;

typedef int16_t int16;
typedef int32_t int32;
typedef int64_t int64;
typedef uint8_t byte;
typedef uint16_t uint16;
typedef uint32_t uint32;
typedef uint64_t uint64;