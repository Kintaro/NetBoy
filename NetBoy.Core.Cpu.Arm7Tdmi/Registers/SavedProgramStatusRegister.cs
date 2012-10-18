using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Core.Cpu.Arm7Tdmi.Registers
{
    /// <summary>
    ///     Whenever the CPU enters an exception, the current status register (CPSR) 
    ///     is copied to the respective SPSR_<mode> register. Note that there is only 
    ///     one SPSR for each mode, so nested exceptions inside of the same mode are 
    ///     allowed only if the exception handler saves the content of SPSR in memory.
    ///     For example, for an IRQ exception: IRQ-mode is entered, and CPSR is copied 
    ///     to SPSR_irq. If the interrupt handler wants to enable nested IRQs, then it 
    ///     must first push SPSR_irq before doing so.
    /// </summary>
    public class SavedProgramStatusRegister : CurrentProgramStatusRegister
    {
    }
}
