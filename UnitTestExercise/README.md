
# MemberService 測試情境

## Register 方法測試情境

### 參數驗證測試

#### IdNumber 驗證
- [ ] **IdNumber 為 null** - 應拋出 ArgumentException，訊息包含 "Id number must be length 10"
- [ ] **IdNumber 為空字串** - 應拋出 ArgumentException，訊息包含 "Id number must be length 10"
- [ ] **IdNumber 為空白字元** - 應拋出 ArgumentException，訊息包含 "Id number must be length 10"
- [ ] **IdNumber 長度小於 10** - 應拋出 ArgumentException，訊息包含 "Id number must be length 10"
- [ ] **IdNumber 長度大於 10** - 應拋出 ArgumentException，訊息包含 "Id number must be length 10"
- [ ] **IdNumber 長度等於 10** - 應通過驗證

#### Name 驗證
- [ ] **Name 為 null** - 應拋出 ArgumentException，訊息包含 "Name cannot be empty"
- [ ] **Name 為空字串** - 應拋出 ArgumentException，訊息包含 "Name cannot be empty"
- [ ] **Name 為有效值** - 應通過驗證

#### Email 驗證
- [ ] **Email 為 null** - 應通過驗證（可選欄位）
- [ ] **Email 為空字串** - 應通過驗證（可選欄位）
- [ ] **Email 為空白字元** - 應通過驗證（可選欄位）
- [ ] **Email 格式無效（缺少@符號）** - 應拋出 ArgumentException，訊息包含 "Email format is invalid"
- [ ] **Email 格式無效（缺少網域名稱）** - 應拋出 ArgumentException，訊息包含 "Email format is invalid"
- [ ] **Email 格式無效（缺少頂級網域）** - 應拋出 ArgumentException，訊息包含 "Email format is invalid"
- [ ] **Email 格式無效（包含空白字元）** - 應拋出 ArgumentException，訊息包含 "Email format is invalid"
- [ ] **Email 格式無效（多個@符號）** - 應拋出 ArgumentException，訊息包含 "Email format is invalid"
- [ ] **Email 格式有效（標準格式）** - 應通過驗證
- [ ] **Email 格式有效（包含數字和特殊字元）** - 應通過驗證
- [ ] **Email 格式有效（大小寫混合）** - 應通過驗證

### 業務邏輯測試

#### 會員存在性檢查
- [ ] **會員已存在** - 應返回 MemberServiceDto，Code 為 StatusEnum.UserIsExits，Message 為對應描述
- [ ] **會員不存在** - 應繼續註冊流程

#### 註冊成功情境
- [ ] **所有參數有效且會員不存在** - 應成功註冊並返回預設的 MemberServiceDto
- [ ] **密碼正確進行 MD5 雜湊** - 驗證密碼是否正確進行 MD5 加密
- [ ] **呼叫 memberRepository.AddMember** - 驗證是否正確呼叫新增會員方法
- [ ] **呼叫 emailRepository.SendRegisterEmail** - 驗證是否正確呼叫發送註冊郵件方法
- [ ] **新增成功後查詢新增的資料並取得密碼** - 驗證密碼是否確實被hash並且hash結果跟預期是否一樣

#### Repository 互動測試
- [ ] **memberRepository.Exists 被正確呼叫** - 驗證傳入正確的 IdNumber
- [ ] **memberRepository.AddMember 被正確呼叫** - 驗證傳入的 GetMemberDto 物件內容正確
- [ ] **emailRepository.SendRegisterEmail 被正確呼叫** - 驗證傳入正確的 Email 和 IdNumber

### 邊界值測試
- [ ] **IdNumber 剛好 10 位數字** - 應通過驗證
- [ ] **IdNumber 剛好 9 位數字** - 應失敗
- [ ] **IdNumber 剛好 11 位數字** - 應失敗

## GetMember 方法測試情境

### 基本功能測試
- [ ] **傳入有效的 IdNumber** - 應返回對應的 GetMemberDto
- [ ] **傳入不存在的 IdNumber** - 應返回 null
### Repository 互動測試
- [ ] **memberRepository.GetMember 被正確呼叫** - 驗證傳入正確的 IdNumber 參數