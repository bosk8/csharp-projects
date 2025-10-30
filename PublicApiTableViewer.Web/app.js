const API_BASE = 'https://jsonplaceholder.typicode.com';

function setBreadcrumbs(pathParts) {
  const el = document.getElementById('breadcrumbs');
  if (!el) return;
  const parts = [];
  if (pathParts.length === 0) {
    el.innerHTML = '';
    return;
  }
  parts.push(`<a class="link" href="#/users">USERS</a>`);
  if (pathParts[0] === 'users' && pathParts[1]) {
    parts.push(`<span class="meta"> / ${String(pathParts[1]).toUpperCase()}</span>`);
  }
  el.innerHTML = parts.join('');
}

function showLoading(container, message = 'Loading') {
  container.innerHTML = `<div class="p-4 meta">${message}…</div>`;
}

function showError(container, message = 'Request failed') {
  container.innerHTML = `<div class="p-4 meta">❌ ${message} — <a class=\"link\" href=\"#\" id=\"retry-link\">Retry</a></div>`;
  const retry = document.getElementById('retry-link');
  if (retry) retry.addEventListener('click', (e) => { e.preventDefault(); router(); });
}

async function fetchJson(url) {
  const res = await fetch(url, { method: 'GET' });
  if (!res.ok) throw new Error(`${res.status}`);
  return await res.json();
}

function renderUsersList(container, users) {
  if (!users || users.length === 0) {
    container.innerHTML = `<div class="p-4 meta">No users found. <a class="link" href="#/users">Retry</a></div>`;
    return;
  }
  const rows = users.map(u => `
    <tr>
      <td>${u.id}</td>
      <td>${escapeHtml(u.name)}</td>
      <td>${escapeHtml(u.username)}</td>
      <td>${escapeHtml(u.email)}</td>
      <td>${escapeHtml(u.address?.city || '')}</td>
      <td>${escapeHtml(u.company?.name || '')}</td>
      <td><a class="link" href="#/users/${u.id}">VIEW</a></td>
    </tr>
  `).join('');
  container.innerHTML = `
    <div class="p-4">
      <div class="table-scroll" role="region" aria-label="Users table">
      <table class="table">
        <thead>
          <tr>
            <th data-key="id">ID</th>
            <th data-key="name">Name</th>
            <th data-key="username">Username</th>
            <th data-key="email">Email</th>
            <th data-key="address.city">City</th>
            <th data-key="company.name">Company</th>
            <th></th>
          </tr>
        </thead>
        <tbody>${rows}</tbody>
      </table>
      </div>
    </div>
  `;

  // bind sort
  const headers = container.querySelectorAll('th[data-key]');
  headers.forEach(h => h.addEventListener('click', () => {
    const key = h.getAttribute('data-key');
    if (CURRENT_VIEW.sort.key === key) CURRENT_VIEW.sort.dir *= -1; else { CURRENT_VIEW.sort.key = key; CURRENT_VIEW.sort.dir = 1; }
    const data = CURRENT_VIEW.filtered || CACHE_USERS || [];
    renderUsersList(container, applySort(data));
  }));
}

function renderUserDetail(container, user) {
  if (!user) {
    container.innerHTML = `<div class="p-4 meta">User not found.</div>`;
    return;
  }
  container.innerHTML = `
    <div class="p-4">
      <div class="meta">OVERVIEW</div>
      <div class="border-b mt-4"></div>
      <div class="mt-4 meta-sm">NAME</div>
      <div>${escapeHtml(user.name)}</div>
      <div class="mt-4 meta-sm">USERNAME</div>
      <div>${escapeHtml(user.username)}</div>
      <div class="mt-4 meta-sm">EMAIL</div>
      <div>${escapeHtml(user.email)}</div>
      <div class="mt-4 meta-sm">PHONE</div>
      <div>${escapeHtml(user.phone)}</div>
      <div class="mt-4 meta-sm">WEBSITE</div>
      <div><a class="link" href="https://${escapeAttr(user.website)}" target="_blank" rel="noreferrer noopener">${escapeHtml(user.website)}</a></div>
      <div class="mt-4 meta">ADDRESS</div>
      <div class="border-b mt-4"></div>
      <div class="mt-4">${escapeHtml(user.address?.street || '')}, ${escapeHtml(user.address?.suite || '')}</div>
      <div>${escapeHtml(user.address?.city || '')}, ${escapeHtml(user.address?.zipcode || '')}</div>
      <div class="mt-4 meta">COMPANY</div>
      <div class="border-b mt-4"></div>
      <div class="mt-4">${escapeHtml(user.company?.name || '')}</div>
      <div class="meta-sm">${escapeHtml(user.company?.catchPhrase || '')}</div>
      <div class="meta-sm">${escapeHtml(user.company?.bs || '')}</div>
      <div class="mt-4"><a class="link" href="#/users">BACK</a></div>
    </div>
  `;
}

function escapeHtml(s) {
  return String(s ?? '')
    .replace(/&/g, '&amp;')
    .replace(/</g, '&lt;')
    .replace(/>/g, '&gt;')
    .replace(/"/g, '&quot;')
    .replace(/'/g, '&#39;');
}
function escapeAttr(s) { return escapeHtml(s).replace(/\s/g, ''); }

let CACHE_USERS = null;
let CURRENT_VIEW = { filtered: null, sort: { key: null, dir: 1 } };

function bindControls() {
  const helpToggle = document.getElementById('help-toggle');
  const helpPanel = document.getElementById('help-panel');
  if (helpToggle && helpPanel) {
    helpToggle.addEventListener('click', () => {
      const expanded = helpToggle.getAttribute('aria-expanded') === 'true';
      helpToggle.setAttribute('aria-expanded', String(!expanded));
      helpPanel.classList.toggle('hidden');
    });
  }

  const refreshBtn = document.getElementById('refresh-btn');
  if (refreshBtn) refreshBtn.addEventListener('click', () => router(true));

  const goBtn = document.getElementById('go-id-btn');
  const goInput = document.getElementById('go-id-input');
  const goHint = document.getElementById('go-id-hint');
  if (goBtn && goInput) {
    const go = () => {
      const v = (goInput.value || '').trim();
      if (/^\d+$/.test(v)) {
        if (goHint) goHint.textContent = 'Enter a numeric user ID (1–10).';
        location.hash = `#/users/${v}`;
      } else {
        if (goHint) goHint.textContent = 'Please enter a numeric ID (e.g., 3).';
        goInput.focus();
      }
    };
    goBtn.addEventListener('click', go);
    goInput.addEventListener('keydown', (e) => { if (e.key === 'Enter') go(); });
    goInput.addEventListener('input', () => { if (goHint) goHint.textContent = 'Enter a numeric user ID (1–10).'; });
  }

  const searchInput = document.getElementById('search-input');
  if (searchInput) {
    searchInput.addEventListener('input', () => {
      const container = document.getElementById('app');
      const q = searchInput.value.toLowerCase();
      if (!CACHE_USERS) return;
      const filtered = CACHE_USERS.filter(u => (
        String(u.id).includes(q) ||
        (u.name||'').toLowerCase().includes(q) ||
        (u.username||'').toLowerCase().includes(q) ||
        (u.email||'').toLowerCase().includes(q)
      ));
      CURRENT_VIEW.filtered = filtered;
      renderUsersList(container, applySort(filtered));
      renderStats(filtered);
    });
  }

  const exportBtn = document.getElementById('export-csv-btn');
  if (exportBtn) exportBtn.addEventListener('click', () => {
    const data = CURRENT_VIEW.filtered || CACHE_USERS || [];
    const csv = toCSV(data);
    const blob = new Blob([csv], { type: 'text/csv;charset=utf-8;' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url; a.download = 'users.csv'; a.click();
    URL.revokeObjectURL(url);
  });

  const copyBtn = document.getElementById('copy-json-btn');
  if (copyBtn) copyBtn.addEventListener('click', async () => {
    const data = CURRENT_VIEW.filtered || CACHE_USERS || [];
    try { await navigator.clipboard.writeText(JSON.stringify(data, null, 2)); copyBtn.textContent = 'COPIED'; setTimeout(()=>copyBtn.textContent='COPY JSON', 1500);} catch {}
  });
}

async function router(forceReload = false) {
  const container = document.getElementById('app');
  const hash = location.hash.replace(/^#\/?/, '');
  const segments = hash.split('/').filter(Boolean);
  setBreadcrumbs(segments);
  bindControls();

  if (segments.length === 0) {
    location.replace('#/users');
    return;
  }

  if (segments[0] === 'users' && !segments[1]) {
    showLoading(container, 'Loading users');
    try {
      if (!CACHE_USERS || forceReload) {
        CACHE_USERS = await fetchJson(`${API_BASE}/users`);
      }
      CURRENT_VIEW.filtered = CACHE_USERS;
      renderUsersList(container, applySort(CACHE_USERS));
      renderStats(CACHE_USERS);
    } catch (e) {
      showError(container, 'Unable to load users');
    }
    return;
  }

  if (segments[0] === 'users' && segments[1]) {
    const id = segments[1];
    showLoading(container, `Loading user ${id}`);
    try {
      const user = await fetchJson(`${API_BASE}/users/${encodeURIComponent(id)}`);
      renderUserDetail(container, user);
    } catch (e) {
      showError(container, `Unable to load user ${id}`);
    }
    return;
  }

  container.innerHTML = `<div class="p-4 meta">Not found. <a class=\"link\" href=\"#/users\">Go to users</a></div>`;
}

// Accessibility helpers
document.addEventListener('keydown', (e) => {
  if (e.key === 'Enter') {
    const target = e.target;
    if (target && target.matches && target.matches('[role="button"]')) {
      target.click();
    }
  }
});

window.addEventListener('hashchange', router);
window.addEventListener('DOMContentLoaded', router);


// Helpers
function deepGet(obj, path) {
  return path.split('.').reduce((o, k) => (o && o[k] != null ? o[k] : ''), obj);
}

function applySort(list) {
  const { key, dir } = CURRENT_VIEW.sort;
  if (!key) return list;
  const sorted = [...list].sort((a, b) => {
    const av = String(key.includes('.') ? deepGet(a, key) : a[key] ?? '').toLowerCase();
    const bv = String(key.includes('.') ? deepGet(b, key) : b[key] ?? '').toLowerCase();
    return av.localeCompare(bv) * dir;
  });
  return sorted;
}

function toCSV(list) {
  const header = ['id','name','username','email','city','company'];
  const lines = [header.join(',')];
  list.forEach(u => {
    const row = [u.id, q(u.name), q(u.username), q(u.email), q(u.address?.city||''), q(u.company?.name||'')].join(',');
    lines.push(row);
  });
  return lines.join('\n');
  function q(v){
    const s = String(v ?? '');
    return /[",\n]/.test(s) ? '"' + s.replace(/"/g,'""') + '"' : s;
  }
}

function renderStats(list){
  const el = document.getElementById('stats');
  if (!el) return;
  if (!list || list.length === 0) { el.innerHTML = '<div class="meta-sm">No data.</div>'; return; }
  const byCity = countBy(list.map(u => u.address?.city || '')); 
  const byCompany = countBy(list.map(u => u.company?.name || ''));
  el.innerHTML = `
    <div class="meta">USERS SUMMARY</div>
    <div class="row mt-4">
      <div class="meta-sm">Total: ${list.length}</div>
    </div>
    <div class="row mt-4">
      ${Object.entries(byCity).map(([k,v])=>`<span class="chip meta-sm">${escapeHtml(k)}: ${v}</span>`).join(' ')}
    </div>
    <div class="row mt-4">
      ${Object.entries(byCompany).map(([k,v])=>`<span class="chip meta-sm">${escapeHtml(k)}: ${v}</span>`).join(' ')}
    </div>
  `;
  function countBy(arr){ return arr.reduce((m,k)=>{ m[k]=(m[k]||0)+1; return m; },{}); }
}
