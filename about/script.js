document.addEventListener('DOMContentLoaded', () => {
  // Dynamic Animated Counters for Stats
  const statNumbers = document.querySelectorAll('.stat-number');

  const animateCounters = () => {
    statNumbers.forEach(counter => {
      const target = +counter.getAttribute('data-target');
      const speed = 50; // lower = faster

      const updateCount = () => {
        const count = +counter.innerText;
        const inc = Math.max(1, Math.ceil(target / speed));

        if (count < target) {
          counter.innerText = count + inc;
          setTimeout(updateCount, 25);
        } else {
          counter.innerText = target;
        }
      };

      updateCount();
    });
  };

  // Trigger counters on scroll into view
  const statsSection = document.querySelector('.stats-grid');
  let animated = false;

  window.addEventListener('scroll', () => {
    if (!statsSection) return;
    const sectionPos = statsSection.getBoundingClientRect().top;
    const screenPos = window.innerHeight / 1.3;

    if (sectionPos < screenPos && !animated) {
      animateCounters();
      animated = true;
    }
  });

  // Initial trigger if hero stats are in view on load
  if (statsSection && statsSection.getBoundingClientRect().top < window.innerHeight) {
    animateCounters();
    animated = true;
  }

  // Smooth Scrolling for Navigation Links
  document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
      e.preventDefault();
      const targetId = this.getAttribute('href');
      if (targetId === '#') return;

      const targetElem = document.querySelector(targetId);
      if (targetElem) {
        targetElem.scrollIntoView({
          behavior: 'smooth',
          block: 'start'
        });
      }
    });
  });
});
