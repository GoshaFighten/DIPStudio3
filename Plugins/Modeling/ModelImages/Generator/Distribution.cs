using System;
using System.Collections.Generic;
using System.Text;

namespace ModelImages.Generator {
    public abstract class Distribution {

        #region instance fields

        /// <summary>
        /// Gets or sets a <see cref="Generator"/> object that can be used as underlying random number generator.
        /// </summary>
        protected Generator Generator {
            get { return this.generator; }
            set { this.generator = value; }
        }

        /// <summary>
        /// Stores a <see cref="Generator"/> object that can be used as underlying random number generator.
        /// </summary>
        private Generator generator;

        /// <summary>
        /// Gets a value indicating whether the random number distribution can be reset, so that it produces the same 
        /// random number sequence again.
        /// </summary>
        public bool CanReset {
            get { return this.generator.CanReset; }
        }

        #endregion

        #region construction

        /// <summary>
        /// Initializes a new instance of the <see cref="Distribution"/> class, using a 
        /// <see cref="StandardGenerator"/> as underlying random number generator.
        /// </summary>
        protected Distribution()
            : this(new StandardGenerator()) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Distribution"/> class, using the specified 
        /// <see cref="Generator"/> as underlying random number generator.
        /// </summary>
        /// <param name="generator">A <see cref="Generator"/> object.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="generator"/> is NULL (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        protected Distribution(Generator generator) {
            if (generator == null) {
                string message = string.Format(null, ExceptionMessages.ArgumentNull, "generator");
                throw new ArgumentNullException("generator", message);
            }
            this.generator = generator;
        }

        #endregion

        #region virtual instance methods

        /// <summary>
        /// Resets the random number distribution, so that it produces the same random number sequence again.
        /// </summary>
        /// <returns>
        /// <see langword="true"/>, if the random number distribution was reset; otherwise, <see langword="false"/>.
        /// </returns>
        public virtual bool Reset() {
            return this.generator.Reset();
        }

        #endregion

        #region abstract members

        /// <summary>
        /// Gets the minimum possible value of distributed random numbers.
        /// </summary>
        public abstract double Minimum { get; }

        /// <summary>
        /// Gets the maximum possible value of distributed random numbers.
        /// </summary>
        public abstract double Maximum { get; }

        /// <summary>
        /// Gets the mean of distributed random numbers.
        /// </summary>
        public abstract double Mean { get; }

        /// <summary>
        /// Gets the median of distributed random numbers.
        /// </summary>
        public abstract double Median { get; }

        /// <summary>
        /// Gets the variance of distributed random numbers.
        /// </summary>
        public abstract double Variance { get; }

        /// <summary>
        /// Gets the mode of distributed random numbers.
        /// </summary>
        public abstract double[] Mode { get; }

        /// <summary>
        /// Returns a distributed floating point random number.
        /// </summary>
        /// <returns>A distributed double-precision floating point number.</returns>
        public abstract double NextDouble();

        #endregion

    }
}
